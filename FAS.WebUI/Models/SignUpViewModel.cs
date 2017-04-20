using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SignUpViewModel
    {
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Captcha")]
        public string Captcha { get; set; }
    }
}