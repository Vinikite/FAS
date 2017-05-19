using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FAS.Domain;
using System;

namespace FAS.WebUI.Models
{
    //public class ExternalLoginConfirmationViewModel
    //{
    //    [Required]
    //    [Display(Name = "Адрес электронной почты")]
    //    public string Email { get; set; }
    //}

    //public class ExternalLoginListViewModel
    //{
    //    public string ReturnUrl { get; set; }
    //}

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}

    //public class VerifyCodeViewModel
    //{
    //    [Required]
    //    public string Provider { get; set; }

    //    [Required]
    //    [Display(Name = "Код")]
    //    public string Code { get; set; }
    //    public string ReturnUrl { get; set; }

    //    [Display(Name = "Запомнить браузер?")]
    //    public bool RememberBrowser { get; set; }

    //    public bool RememberMe { get; set; }
    //}

    //public class ForgotViewModel
    //{
    //    [Required]
    //    [Display(Name = "Адрес электронной почты")]
    //    public string Email { get; set; }
    //}

    public class LoginViewModel
    {
        //[Required]
        [Display(Name = "Адрес электронной почты")]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Captcha")]
        public string Captcha { get; set; }
    }

    //public class ResetPasswordViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Адрес электронной почты")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Пароль")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Подтверждение пароля")]
    //    [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
    //    public string ConfirmPassword { get; set; }

    //    public string Code { get; set; }
    //}

    //public class ForgotPasswordViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Почта")]
    //    public string Email { get; set; }
    //}


    ///////////////////////////////
    public class SimpleUserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid? IdAddress { get; set; }
        public double AverageIncome { get; set; }

        //не уверен
        public virtual Address Address { get; set; }

        //
        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateUserViewModel
    {
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Display(Name = "MiddleName")]
        public string MiddleName { get; set; }
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "AverageIncome")]
        public double AverageIncome { get; set; }
    }

    public class ChangeUserViewModel : CreateUserViewModel
    {
        public Guid Id { get; set; }
        public Guid IdAddress { get; set; }
    }
}
