using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class Category : AppEntity
    {
        public Category()
        {
            Transactions = new HashSet<Transaction>();
        }
        public Category(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
