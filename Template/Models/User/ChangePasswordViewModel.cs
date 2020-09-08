namespace Template.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Old password is required!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required!")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewRepeatPassword), ErrorMessage = "Repeated password did not match")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please repeat new password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string NewRepeatPassword { get; set; }
    }
}