namespace BankProject.Models.ViewModels
{
    public class TransferViewModel
    {
        public string FirstUserId { get; set; }   
        public string SecondUserId { get; set; } 
        public decimal TransferredAmount { get; set; }       
        public decimal CurrentBalance { get; set; }  

    }
}
