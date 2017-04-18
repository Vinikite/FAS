using System;

namespace FAS.Core
{
    public abstract class AppEntity : IAppEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifyOn { get; set; }
    }
}
