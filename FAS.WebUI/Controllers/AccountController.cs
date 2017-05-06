using Microsoft.Owin.Security;
using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using FAS.BLL;
using FAS.Domain;
using FAS.WebUI.Infrastructure;
using FAS.WebUI.Models;
using System.Linq;

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

                if (user == null)
                {
                    ModelState.AddModelError("Login", "Пользователь с таким email не зарегистрирован.");
                    return View(model);
                }

                var userWP = await userService.FindAsync(model.Login, model.Password);

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

                return RedirectToAction("Index", "Home");
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
            var isCaptchaValid = !String.IsNullOrEmpty(Session[Captcha.CaptchaKey].ToString()) &&
                Session[Captcha.CaptchaKey].ToString().Equals(model.Captcha);

            model.Captcha = String.Empty;

            if (ModelState.IsValid)
            {
                if (isCaptchaValid)
                {
                    var user = await userService.GetByEmailAsync(model.Login);

                    if (user != null)
                    {
                        ModelState.AddModelError("Email", "Пользователь с таким email уже существует.");
                        return View(model);
                    }


                    user = new User()
                    { 
                        UserName =  model.Login,
                        PasswordHash = model.Password,
                        Email = model.Login
                    };
                    user.UserName = model.Login;

                    var createResult = await userService.CreateWithInfoAsync(user);

                    if (createResult.Succeeded)
                    {
                        // send email to user 
                        return RedirectToAction("Success");
                    }

                    ModelState.AddModelError("Email", createResult.Errors.Aggregate(String.Empty, (a, i) => a += i));
                    return View(model);
                }

                ModelState.AddModelError("Captcha", "Неверный код с картинки");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Success()
        {
            return View();
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
    }
}