using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.Common;
using UserAPI.Common.Dto;
using UserAPI.Common.Utility;
using UserAPI.Data;
using UserAPI.Entities;
using UserAPI.Services.Dto;
using static UserAPI.Common.Utility.SocialLoginEnum;

namespace UserAPI.Services
{
    public class UserAppService : AsyncCrudAppServiceBase<AppUser, AppUserDto, int, GetAllInputDto, AppUserDto, AppUserDto>, IUserAppService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserAppService(UserDbContext _dbContext, IMapper _mapper,
            UserManager<IdentityUser> userManager, IConfiguration configuration) : base(_dbContext, _mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ResponseDto> Login(LoginInputDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return new ResponseDto(false, SocialLoginConst.UserNotFound);
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var jwtToken = await GenerateJwtToken(user);

                return new ResponseDto(true, SocialLoginConst.LoginSuccessfully, jwtToken);
            }

            return new ResponseDto(false, SocialLoginConst.LoginFailure);
        }

        public async Task<ResponseDto> Register(RegisterInputDto input)
        {
            var userExists = await _userManager.FindByNameAsync(input.Username);

            if (userExists != null)
            {
                return new ResponseDto(false, SocialLoginConst.UserAlreadyExists);
            }

            return await CreateUser(input);
        }

        public async Task<ResponseDto> LoginWithGoogle(SocialLoginInputDto input)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(input.IdToken);

            if (payload == null)
            {
                return new ResponseDto(false, SocialLoginConst.SomethingWentWrong);
            }

            var userExists = await _userManager.FindByNameAsync(payload.Name.Replace(" ", ""));

            if (userExists == null)
            {
                var createUserInput = new RegisterInputDto { Email = payload.Email, Password = SocialLoginConst.GoogleLoginProvider, Role = UserRoles.User.ToString(), Username = payload.Name.Replace(" ", "") };
                var response = await CreateUser(createUserInput);

                if (!response.Success)
                {
                    return response;
                }

                userExists = response.Data;
            }

            var jwtToken = await GenerateJwtToken(userExists);

            return new ResponseDto(true, SocialLoginConst.LoginSuccessfully, jwtToken);
        }

        public async Task<ResponseDto> LoginWithFacebook(SocialLoginInputDto input)
        {
            try
            {
                var httpClient = new HttpClient();
                var tokenUrl = $"{_configuration["SocialLogin:Facebook:AccessTokenValidationAPI"]}{input.IdToken}";
                var response = await httpClient.GetAsync(tokenUrl);

                if (response.IsSuccessStatusCode)
                {
                    var fetchResponseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FacebookResponseDto>(fetchResponseData);

                    var userExists = await _userManager.FindByNameAsync(result.Name.Replace(" ", ""));

                    if (userExists == null)
                    {
                        var createUserInput = new RegisterInputDto { Email = result.Email, Password = SocialLoginConst.FacebookLoginProvider, Role = UserRoles.User.ToString(), Username = result.Name.Replace(" ", "") };
                        var createUserResponse = await CreateUser(createUserInput);

                        if (!createUserResponse.Success)
                        {
                            return createUserResponse;
                        }

                        userExists = createUserResponse.Data;
                    }

                    var jwtToken = await GenerateJwtToken(userExists);

                    return new ResponseDto(true, SocialLoginConst.LoginSuccessfully, jwtToken);
                }

                return new ResponseDto(false, SocialLoginConst.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<JwtTokenDto> GenerateJwtToken(IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtTokenDto(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        private async Task<ResponseDto> CreateUser(RegisterInputDto model)
        {
            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new ResponseDto(false, SocialLoginConst.UserCreationFailed);
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            return new ResponseDto(true, SocialLoginConst.UserCreationSuccess, user);
        }
    }
}
