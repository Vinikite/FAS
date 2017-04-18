using FAS.Core;
using System;
using System.Data.Entity;

namespace FAS.DAL.Repository
{
    public class AppRepository<TEntity> : Repository<TEntity, Guid>, IAppRepository<TEntity>
        where TEntity : class, IAppEntity<Guid>
    {
        public AppRepository(DbContext context) : base(context) { }
    }
}
