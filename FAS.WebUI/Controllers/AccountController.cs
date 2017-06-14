using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using FAS.WebUI.Models;
using FAS.BLL;
using FAS.WebUI.Infrastructure;
using FAS.Domain;
using System.Drawing.Imaging;

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
       
      
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Login", "Пользователь с таким email не зарегистрирован.");
                    return View(model);
                }

                var userWP = await userService.FindAsync(model.Email, model.Password);

                if (userWP == null)
                {
                    ModelState.AddModelError("Login", "Логин или пароль введен не верно.");
                    ModelState.AddModelError("Password", "Логин или пароль введен не верно.");

                    return View(model);
                }

                var claims = await userService.CreateIdentityAsync(userWP);
                var authProperties = new AuthenticationProperties
                {
                    IssuedUtc = DateTimeOffset.UtcNow,
                    IsPersistent = true
                };

                authManager.SignOut();
                authManager.SignIn(authProperties, claims);

                return RedirectToAction("IndexHome", "Home");
            }

            return View(model);
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
             [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var isCaptchaValid = !String.IsNullOrEmpty(Session[Captcha.CaptchaKey].ToString()) &&
                Session[Captcha.CaptchaKey].ToString().Equals(model.Captcha);

            model.Captcha = String.Empty;
            if (ModelState.IsValid)
            {
                if (isCaptchaValid)
                {
                    var user = await userService.GetByEmailAsync(model.Email);

                    if (user != null)
                    {
                        ModelState.AddModelError("Email", "Пользователь с таким email уже существует.");
                        return View(model);
                    }

                    user = new User()
                    {
                        UserName = model.Email,
                        PasswordHash = model.Password,
                        Email = model.Email
                    };
                    user.UserName = model.Email;

                    var createResult = await userService.CreateWithInfoAsync(user);

                    if (createResult.Succeeded)
                    {
                        // send email to user 
                        return RedirectToAction("IndexHome","Home");
                    }

                    ModelState.AddModelError("Email", createResult.Errors.Aggregate(String.Empty, (a, i) => a += i));
                    return View(model);
                }

                ModelState.AddModelError("Captcha", "Неверный код с картинки");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Запрос перенаправления к внешнему поставщику входа
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
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

        //

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}