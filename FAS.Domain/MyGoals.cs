using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class MyGoals : AppEntity
    {
        public MyGoals()
        {
            Transactions = new HashSet<Transaction>();
        }
        public MyGoals(string Name, double Price)
        {
            this.Name = Name;
            this.Price = Price;
        }

        public string Name { get; set; }
        public double  Price { get; set; }


        public ICollection<Transaction> Transactions { get; set; }
    }
}
