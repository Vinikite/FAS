using FAS.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleScoreViewModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdViewScore { get; set; }
        public Guid IdTypeScore { get; set; }
        public Guid IdStatus { get; set; }
        public double Balance { get; set; }
        public string Notation { get; set; }
        //не уверен
        public virtual User User { get; set; }
        public virtual ViewScore ViewScore { get; set; }
        public virtual TypeScore TypeScore { get; set; }
        public virtual StatusScore StatusScore { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateScoreViewModel
    {
        [Display(Name = "Balance")]
        public double Balance { get; set; }
        [Display(Name = "Notation")]
        public string Notation { get; set; }
    }

    public class ChangeScoreViewModel : CreateScoreViewModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdViewScore { get; set; }
        public Guid IdTypeScore { get; set; }
        public Guid IdStatus { get; set; }
    }
}