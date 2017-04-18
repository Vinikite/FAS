using FAS.Domain;
using Microsoft.AspNet.Identity;
using System;

namespace FAS.DAL.Identity
{
    public class AppUserManager : UserManager<User, Guid>
    {
        public AppUserManager(IUserStore<User, Guid> userStore) : base(userStore) { }
    }
}
