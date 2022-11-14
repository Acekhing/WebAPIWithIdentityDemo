using System.ComponentModel.DataAnnotations;

namespace WebAPIWithIdentityDemo.DTOs
{
    public class RegisterDTO: LoginDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}