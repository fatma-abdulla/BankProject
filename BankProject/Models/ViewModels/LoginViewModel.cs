using System.ComponentModel.DataAnnotations;

namespace BankProject.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Email Address")]
        [Display(Name = "Email")]
        [EmailAddress]
        [MinLength(5)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
