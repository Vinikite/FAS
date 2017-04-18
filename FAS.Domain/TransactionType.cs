using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class TransactionType : AppEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
