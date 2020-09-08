namespace Template.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid Email address")]
        public string Email { get; set; }

        [Required]
        public string Firstname { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum length of your password needs to be 6 characters long")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords didn't match")]
        [MinLength(6, ErrorMessage = "Minimum length of your password needs to be 6 characters long")]
        public string PasswordCheck { get; set; }
    }
}
