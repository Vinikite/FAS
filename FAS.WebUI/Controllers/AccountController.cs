using Microsoft.Owin.Security;
using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using FAS.Core;
using FAS.Domain;
using FAS.WebUI.Infrastructure;
using FAS.WebUI.Models;

namespace FAS.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationManager authManager;

        public AccountController(IUserService userService, IAuthenticationManager authManager)
        {
            this.userService = userService;
            this.authManager = authManager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetByEmailAsync(model.Login);

                if (user == null || !Crypto.VerifyHashedPassword(user.PasswordHash, model.Password))
                {
                    ModelState.AddModelError(String.Empty, "Неверный логин или пароль");
                    return View(model);
                }
                else
                {
                    var claims = await userService.CreateIdentityAsync(user);

                    authManager.SignOut();
                    authManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claims);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!Session[Captcha.CaptchaKey].Equals(model.Captcha))
                {
                    ModelState.AddModelError("Captcha", "Incorrect code");
                    return View(model);
                }

                var user = await userService.GetByEmailAsync(model.Login);

                if (user != null)
                {
                    ModelState.AddModelError("Users", "User with that email exists");
                    return View(model);
                }

                user = new User
                {
                    Email = model.Login,
                    PasswordHash = Crypto.HashPassword(model.Password),
                    UserName = model.Login
                };

                var details = await userService.Create(user);

                if (!details.Succedeed)
                {
                    ModelState.AddModelError(details.Property, details.Message);
                    return View(model);
                }

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CaptchaImage()
        {
            var text = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            Session[Captcha.CaptchaKey] = text;
            Captcha captcha = new Captcha(text, 211, 50, "Helvetica");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            captcha.Dispose();

            return null;
        }

        [Authorize]
        [ClaimPermission("ViewClaims", UnlessRole = "Admin")]
        async public Task<ActionResult> Claims()
        {
            var user = await userService.GetByEmailAsync(HttpContext.User.Identity.Name);

            return View(await userService.CreateIdentityAsync(user));
        }

        [Authorize]
        async public Task<ActionResult> GetAccess()
        {
            var user = await userService.GetByEmailAsync(HttpContext.User.Identity.Name);
            var claim = new Claim(ClaimPermissionAttribute.MethodClaimType, "ViewClaims");

            await userService.AddClaimAsync(user.Id, claim);

            return RedirectToAction("Claims");
        }

    }
}