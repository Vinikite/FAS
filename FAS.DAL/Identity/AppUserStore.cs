using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using FAS.Domain;

namespace FAS.DAL.Identity
{
    public class AppUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public AppUserStore(DbContext context) : base(context) { }
    }
}
