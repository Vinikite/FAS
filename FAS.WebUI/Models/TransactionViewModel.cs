using FAS.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleTransactionViewModel
    {
        public Guid Id { get; set; }
        public Guid IdTransactionType { get; set; }
        public Guid IdScore { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdBank { get; set; }
        public double Comission { get; set; }
        public string Notation { get; set; }

        //не уверен
        public virtual TransactionType TransactionType { get; set; }
        public virtual Score Score { get; set; }
        public virtual Category Category { get; set; }
        public virtual Bank Bank { get; set; }
        //
        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateTransactionViewModel
    {
        [Display(Name = "Comission")]
        public double Comission { get; set; }
        [Display(Name = "Notation")]
        public string Notation { get; set; }
    }

    public class ChangeTransactionViewModel : CreateTransactionViewModel
    {
        public Guid Id { get; set; }
        public Guid IdTransactionType { get; set; }
        public Guid IdScore { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdBank { get; set; }
    }
}