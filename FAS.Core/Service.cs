using System;
using System.Linq;
using System.Threading.Tasks;

namespace FAS.Core
{
    public abstract class Service<TEntity, TKey> where TEntity : class, IAppEntity<TKey>
    {
        private readonly IRepository<TEntity, TKey> repository;
        private readonly IUnitOfWork unitOfWork;

        public Service(IRepository<TEntity, TKey> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        protected IRepository<TEntity, TKey> Repository => repository;
        protected IUnitOfWork UnitOfWork => unitOfWork;

        public async virtual Task CreateAsync(TEntity entity)
        {
            repository.Add(entity);
            await unitOfWork.CommitAsync();
        }

        public virtual TEntity Get(TKey key)
        {
            return repository.Get(key);
        }

        public virtual IQueryable<TEntity> Get()
        {
            return repository.Get();
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            repository.Update(entity);
            await unitOfWork.CommitAsync();
        }

        public async virtual Task DeleteAsync(TKey key)
        {
            var entity = Get(key);

            if (entity == null)
            {
                throw new ArgumentOutOfRangeException(nameof(key), $"Entity with key {key} not found.");
            }

            repository.Delete(entity);
            await unitOfWork.CommitAsync();
        }
    }
}
