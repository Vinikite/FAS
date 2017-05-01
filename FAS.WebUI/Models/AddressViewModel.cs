using System;
using System.ComponentModel.DataAnnotations;

namespace FAS.WebUI.Models
{
    public class SimpleAddressViewModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteOn { get; set; }
    }

    public class CreateAddressViewModel
    {
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Display(Name = "House")]
        public string House { get; set; }
        [Display(Name = "Flat")]
        public string Flat { get; set; }
    }

    public class ChangeAddressViewModel : CreateAddressViewModel
    {
        public Guid Id { get; set; }
    }
}