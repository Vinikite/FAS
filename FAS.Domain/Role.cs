using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace FAS.Domain
{
    public class Role : IdentityRole<Guid, UserRole> { }
}
