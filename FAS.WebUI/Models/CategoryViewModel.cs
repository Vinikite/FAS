using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Notation { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateCategoryViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Notation")]
        public decimal Notation { get; set; }
    }

    public class ChangeCategoryViewModel : CreateCategoryViewModel
    {
        public Guid Id { get; set; }
    }
}