using System;

namespace FAS.Core
{
    public interface IAppEntity<TKey> : IEntity<TKey>
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifyOn { get; set; }
    }
}
