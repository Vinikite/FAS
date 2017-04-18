using FAS.Domain;
using Microsoft.AspNet.Identity;
using System;

namespace FAS.DAL.Identity
{
    public class AppRoleManager : RoleManager<Role, Guid>
    {
        public AppRoleManager(IRoleStore<Role, Guid> roleStore)
            : base(roleStore) { }
    }
}
