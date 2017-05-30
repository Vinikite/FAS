using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleMyGoalsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateMyGoalsViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Сумма")]
        public double Price { get; set; }
    }

    public class ChangeMyGoalsViewModel : CreateMyGoalsViewModel
    {
        public Guid Id { get; set; }
    }
}