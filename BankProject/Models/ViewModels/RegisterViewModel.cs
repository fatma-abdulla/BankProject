using System.ComponentModel.DataAnnotations;
using static BankProject.Models.ApplicationUser;
namespace BankProject.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress]
        [MinLength(6)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Confirm Email Address")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email not match")]
        public string ConfirmEmail { get; set; }


        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not match")]

        public string ConfirmPassword { get; set; }

        public string? Mobile { get; set; }
        public Genders Gender { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        public string Name { get; set; }    
        public decimal balance { get; set; }
    }
}
