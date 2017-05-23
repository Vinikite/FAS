using FAS.BLL;
using FAS.Domain;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FAS.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IUserService userService;
        private User currentUser;
        public AppController() { }

        public AppController(IUserService userService)
        {
            this.userService = userService;
        }

        protected IUserService UserService => userService;
        protected bool IsUserAuthenticated => User.Identity.IsAuthenticated;
        protected string UserName => User.Identity.Name;
        
        protected async Task<User> GetCurrentUserAsync()
        {
            if (currentUser != null)
            {
                return currentUser;
            }

            currentUser = await userService.GetByEmailAsync(UserName);

            return currentUser;
        }
        protected User GetCurrentUser()
        {
            if (currentUser != null)
            {
                return currentUser;
            }

            currentUser = userService.GetByEmail(UserName);

            return currentUser;
        }
    }
}