using FAS.Core;
using System;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class Score : AppEntity
    {
        public Score()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid IdUser { get; set; }
        public Guid IdViewScore { get; set; }
        public Guid IdTypeScore { get; set; }
        public Guid IdStatus { get; set; }
        public double Balance { get; set; }
        public string Notation { get; set; }

        public virtual User User { get; set; }
        public virtual ViewScore ViewScore { get; set; }
        public virtual TypeScore TypeScore { get; set; }
        public virtual StatusScore StatusScore { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
