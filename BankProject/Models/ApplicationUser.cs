using Microsoft.AspNetCore.Identity;

namespace BankProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public decimal balance { get; set; }
        public DateTime DateOfBirth { get; set; }   

        public Genders Gender { get ; set; }    
        public enum Genders { male, female}
    }
}
