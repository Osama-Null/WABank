using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WABank.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; } // Primary Key

        [Required(ErrorMessage ="Enter The Depost Amount ")]
        [DisplayName("Depost")]

        public decimal Deposit { get; set; } // Amount deposited

        [Required(ErrorMessage = "Enter The Withdraw Amount ")]
        public decimal Withdraw { get; set; } // Amount withdrawn

        [Required(ErrorMessage = "Enter The Transfair Amount ")]
        [DisplayName("Tranfer")]
        public int? Transfer { get; set; } // Nullable Foreign Key to related transfer transaction
        public DateTime Date { get; set; } // Transaction date

        [Required(ErrorMessage = "Enter The Amount you Need ")]
        public decimal Amount { get; set; } // Total transaction amount
        [Required]
        // ------------------- Join Usr & Role
        [DisplayName("User ID")]
        [ForeignKey("User")]
        public string UserId { get; set; } // Foreign Key to User
        public AppUser? User { get; set; } // Navigation property
    }
}
