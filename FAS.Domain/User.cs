using FAS.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>, IAppEntity<Guid>
    {
        private string v;

        public User()
        {
            Scores = new HashSet<Score>();
        }

        public User(string FirstName,string LastName,string MiddleName,string UserName, double AverageIncome)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            this.UserName = Email;
            this.AverageIncome = AverageIncome;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid? IdAddress { get; set; }
        public double AverageIncome { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifyOn { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
