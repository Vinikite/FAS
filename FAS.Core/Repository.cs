﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FAS.Core
{
    public abstract class Repository<TEntity, TKey> where TEntity : class, IAppEntity<TKey>
    {
        private DbContext context;
        private IDbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        protected DbContext Context => context;

        public virtual void Add(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
            dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async virtual Task<TEntity> GetAsync(TKey id)
        {
            return await dbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public virtual IQueryable<TEntity> Get()
        {
            return dbSet;
        }
    }
}
