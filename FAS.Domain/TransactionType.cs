using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class TransactionType : AppEntity
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }
        public TransactionType(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
