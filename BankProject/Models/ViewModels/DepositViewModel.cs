using System.ComponentModel.DataAnnotations.Schema;

namespace BankProject.Models.ViewModels
{
    public class DepositViewModel
    {
        [ForeignKey("RegisterViewModel")]
        public string UserId { get; set; }
        public string FirstUserId {  get; set; }    
        public string SecondUserId { get; set; }
        public decimal balance { get ; set; }    
        public decimal Newbalance { get ; set; }    
    }
}
