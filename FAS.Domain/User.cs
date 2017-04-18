using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid IdAddress { get; set; }
        public double AverageIncome { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
