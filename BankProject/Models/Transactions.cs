using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BankProject.Models
{
    public class Transactions
    {
        public int TransactionsId { get; set; }
        public string UserId { get; set; }
        public decimal OldBalance { get; set; }
        public decimal UpdeatedBalance { get; set; }
        public DateTime TransationDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Newbalance { get; set; } 
    }
}
