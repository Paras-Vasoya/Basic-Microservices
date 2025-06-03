using System.ComponentModel.DataAnnotations;

namespace UserAPI.Services.Dto
{
    public class LoginInputDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
