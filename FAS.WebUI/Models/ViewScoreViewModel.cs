using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleViewScoreViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateViewScoreViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class ChangeViewScoreViewModel : CreateViewScoreViewModel
    {
        public Guid Id { get; set; }
    }
}