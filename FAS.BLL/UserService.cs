using FAS.BLL.Infrastructure;
using FAS.DAL.Identity;
using FAS.Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FAS.BLL
{
    public interface IUserService
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        User GetByEmail(string email);
        IQueryable<User> GetAll();
        Task<OperationDetails> Create(User user);
        Task Delete(Guid id);
        Task Delete(User user);
        Task Update(User user);
        Task<IdentityResult> AddClaimAsync(Guid userId, Claim claim);
        Task<IdentityResult> RemoveClaimAsync(Guid userId, Claim claim);
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<ClaimsIdentity> CreateIdentityAsync(User user);
        ClaimsIdentity CreateIdentity(User user);
        Task<User> Find(string login, string password);
    }

    public class UserService : IUserService
    {
        private readonly AppUserManager userManager;
        private readonly AppRoleManager roleManager;

        public UserService(AppUserManager userManager, AppRoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> AddClaimAsync(Guid userId, Claim claim)
        {
            return await userManager.AddClaimAsync(userId, claim);
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public User GetByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }

        public IQueryable<User> GetAll()
        {
            return userManager.Users;
        }

        public async Task<OperationDetails> Create(User user)
        {
            var dbUser = await GetByEmailAsync(user.Email);
            if (dbUser == null)
            {
                var res = await userManager.CreateAsync(user);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user.Id, "User");

                    return new OperationDetails(true, "Пользователь успешно создан", String.Empty);
                }

                return new OperationDetails(false, res.Errors.Aggregate(String.Empty,
                                                                        (acum, elem) => acum += elem + "; "),
                                                                        "Internal error");
            }

            return new OperationDetails(false, "Пользователь с атким Email уже существует.", "Email");
        }

        public async Task Delete(Guid id)
        {
            var user = await GetAsync(id);
            await Delete(user);
        }

        public async Task Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await userManager.DeleteAsync(user);
        }

        public async Task Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> RemoveClaimAsync(Guid userId, Claim claim)
        {
            return await userManager.RemoveClaimAsync(userId, claim);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(User user)
        {
            var claims = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            foreach (var userRole in user.Roles)
            {
                var role = roleManager.Roles.FirstOrDefault(f => f.Id == userRole.RoleId);

                if (role != null)
                {
                    var claim = new Claim(ClaimTypes.Role, role.Name);
                    claims.AddClaim(claim);
                }
            }

            return claims;
        }

        public async Task<User> Find(string login, string hashPassword)
        {
            return await userManager.Users.FirstOrDefaultAsync(u => u.Email.Equals(login) &&
                            u.PasswordHash.Equals(hashPassword));
        }

        public ClaimsIdentity CreateIdentity(User user)
        {
            var claims = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            foreach (var userRole in user.Roles)
            {
                var role = roleManager.Roles.FirstOrDefault(f => f.Id == userRole.RoleId);

                if (role != null)
                {
                    var claim = new Claim(ClaimTypes.Role, role.Name);
                    claims.AddClaim(claim);
                }
            }

            return claims;
        }
    }
}
