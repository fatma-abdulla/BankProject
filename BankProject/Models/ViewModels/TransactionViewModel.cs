namespace BankProject.Models.ViewModels
{
    public class TransactionViewModel
    {
        public string UserName { get; set; }
        public decimal? NewBalance { get; set; }
        public List<Transactions> Transactions { get; set; }

    }
}
