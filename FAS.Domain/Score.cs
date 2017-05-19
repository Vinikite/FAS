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
        public Score(Guid IdUser, Guid IdViewScore, Guid IdTypeScore, Guid IdStatus, double Balance, string Notation)
        {
            this.IdUser = IdUser;
            this.IdViewScore = IdViewScore;
            this.IdTypeScore = IdTypeScore;
            this.IdStatus = IdStatus;
            this.Balance = Balance;
            this.Notation = Notation;
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
