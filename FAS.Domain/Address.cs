using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class Address : AppEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
