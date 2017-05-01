using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using FAS.Domain;

namespace FAS.DAL.Identity
{
    public class AppRoleStore : RoleStore<Role, Guid, UserRole>
    {
        public AppRoleStore(DbContext context) : base(context) { }
    }
}
