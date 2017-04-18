using FAS.Core;
using System;

namespace FAS.DAL.Repository
{
    public interface IAppRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : IAppEntity<Guid> { }
}
