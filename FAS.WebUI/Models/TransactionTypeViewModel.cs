using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleTransactionTypeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateTransactionTypeViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class ChangeTransactionTypeViewModel : CreateTransactionTypeViewModel
    {
        public Guid Id { get; set; }
    }
}