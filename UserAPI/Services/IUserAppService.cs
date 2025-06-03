using UserAPI.Common;
using UserAPI.Common.Dto;
using UserAPI.Services.Dto;

namespace UserAPI.Services
{
    public interface IUserAppService : IAsyncCrudAppServiceBase<AppUserDto, int, GetAllInputDto, AppUserDto, AppUserDto>
    {
        Task<ResponseDto> Login(LoginInputDto model);
        Task<ResponseDto> Register(RegisterInputDto model);
        Task<ResponseDto> LoginWithGoogle(SocialLoginInputDto input);
        Task<ResponseDto> LoginWithFacebook(SocialLoginInputDto input);
    }
}
