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
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }


        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not match")]
        [Display(Name = "Confirm Password")]

        public string ConfirmPassword { get; set; }

        public string? Mobile { get; set; }
        public Genders Gender { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]

        public DateOnly DateOfBirth { get; set; }
        public string Name { get; set; }    
        public decimal balance { get; set; }
        [Display(Name = "Display Profile Picture")]

        public IFormFile? ProfileImage { get; set; }
    }
}
