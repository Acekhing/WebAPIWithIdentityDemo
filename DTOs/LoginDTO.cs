

using System.ComponentModel.DataAnnotations;
using WebAPIWithIdentityDemo.Data;

namespace WebAPIWithIdentityDemo.DTOs
{
    public class LoginDTO
    {

        [Required, MaxLength(256, ErrorMessage = "Username cannot be more than 256 charaters long")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(
            maximumLength: 256,
            ErrorMessage = $"Password length too short", 
            MinimumLength = DatabaseContext.DevelopmentMinimumPasswordLength
        )]
        public string Password { get; set; }
    }
}
