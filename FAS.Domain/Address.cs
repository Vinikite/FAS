using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class Address : AppEntity
    {
        public Address()
        {
            Users = new HashSet<User>();
        }

        public Address(string Country,string City, string Street,string House,string Flat)
        {
            this.Country = Country;
            this.City = City;
            this.Street = Street;
            this.House = House;
            this.Flat = Flat;
        }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
