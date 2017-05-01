using Microsoft.AspNet.Identity;
using System;
using FAS.Domain;

namespace FAS.DAL.Identity
{
    public class AppUserManager : UserManager<User, Guid>
    {
        public AppUserManager(IUserStore<User, Guid> userStore) : base(userStore) { }
    }
}
