using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BankProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }    
        public decimal balance { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }   

        public Genders Gender { get ; set; }    
        public enum Genders { male, female}

    }
}
