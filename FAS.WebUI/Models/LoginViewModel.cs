using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}