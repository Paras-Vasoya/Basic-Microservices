using Microsoft.AspNetCore.Mvc;
using UserAPI.Services;
using UserAPI.Services.Dto;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _UserService;
        public UserController(IUserAppService userService)
        {
            _UserService = userService;
        }

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<List<AppUserDto>>> GetAll()
        //{
        //    var result = await _UserService.GetAllAsync();
        //    return Ok(result);
        //}

        //[HttpGet("GetById/{id}")]
        //public async Task<ActionResult<AppUserDto>> GetById(int id)
        //{
        //    var result = await _UserService.GetByIdAsync(id);
        //    return result == null ? NotFound() : Ok(result);
        //}

        //[HttpPost("Create")]
        //public async Task<ActionResult<AppUserDto>> CreateOrUpdate(AppUserDto productDto)
        //{
        //    var result = await _UserService.CreateAsync(productDto);
        //    return Ok(result);
        //}

        //[HttpPut("Update")]
        //public async Task<ActionResult<AppUserDto>> Update(AppUserDto productDto)
        //{
        //    var result = await _UserService.CreateAsync(productDto);
        //    return Ok(result);
        //}

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _UserService.DeleteAsync(id);
        //    return NoContent();
        //}
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginInputDto input)
        {
            return Ok(await _UserService.Login(input));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterInputDto input)
        {
            return Ok(await _UserService.Register(input));
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> GoogleLogin(SocialLoginInputDto input)
        {
            return Ok(await _UserService.LoginWithGoogle(input));
        }

        [HttpPost("login/facebook")]
        public async Task<IActionResult> FacebookLogin(SocialLoginInputDto input)
        {
            return Ok(await _UserService.LoginWithFacebook(input));
        }
    }
}
