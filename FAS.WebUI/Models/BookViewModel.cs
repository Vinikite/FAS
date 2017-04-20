using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleBookViewModel//проверка 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateBookViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }

    public class ChangeBookViewModel : CreateBookViewModel
    {
        public Guid Id { get; set; }
    }
}