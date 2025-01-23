using static BankProject.Models.ApplicationUser;
using System.ComponentModel.DataAnnotations;

namespace BankProject.Models.ViewModels
{
    public class EditViewModel
    { 
        public string? Mobile { get; set; }
        public Genders Gender { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        public string Name { get; set; }
        public decimal balance { get; set; }
    }
}
