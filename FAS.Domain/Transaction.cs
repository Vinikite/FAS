using FAS.Core;
using System;

namespace FAS.Domain
{
    public class Transaction : AppEntity
    {
        public Guid IdTransactionType { get; set; }
        public Guid IdScore { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdBank { get; set; }
        public double Comission { get; set; }
        public string Notation { get; set; }

        public virtual TransactionType TransactionType { get; set; }
        public virtual Score Score { get; set; }
        public virtual Category Category { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
