using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class Bank : AppEntity
    {
        public Bank()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string Name { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
