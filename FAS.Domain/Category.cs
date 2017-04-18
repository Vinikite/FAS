using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class Category : AppEntity
    {
        public string Name { get; set; }
        public string Notation { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
