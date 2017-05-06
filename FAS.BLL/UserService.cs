using FAS.Core;
using FAS.DAL.Identity;
using FAS.Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FAS.BLL
{
    public interface IUserService : IService<User, Guid>
    {
        User GetByEmail(string email);
        Task<User> GetByEmailAsync(string email);
        Task<IdentityResult> CreateWithInfoAsync(User entity);
        Task<ClaimsIdentity> CreateIdentityAsync(User user);
        Task<User> FindAsync(string userName, string password);
        Task<IEnumerable<string>> GetRolesAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly AppUserManager userManager;

        public UserService(AppUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateAsync(User entity)
        {
            await createWithInfoAsync(entity);
        }

        public async Task<IdentityResult> CreateWithInfoAsync(User entity)
        {
            return await createWithInfoAsync(entity);
        }

        private async Task<IdentityResult> createWithInfoAsync(User entity)
        {
            var res = await userManager.CreateAsync(entity, entity.PasswordHash);

            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(entity.Id, "User");
            }

            return res;
        }

        public async Task DeleteAsync(Guid key)
        {
            var entity = await GetAsync(key);

            if (entity != null)
            {
                await userManager.DeleteAsync(entity);
            }
        }

        public IQueryable<User> Get()
        {
            return userManager.Users;
        }

        public async Task<User> GetAsync(Guid key)
        {
            return await userManager.FindByIdAsync(key);
        }

        public User GetByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task UpdateAsync(User entity)
        {
            await userManager.UpdateAsync(entity);
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(User user)
        {
            return await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<User> FindAsync(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(User user)
        {
            return await userManager.GetRolesAsync(user.Id);
        }
    }
}
