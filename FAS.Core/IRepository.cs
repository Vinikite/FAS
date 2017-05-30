using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAS.Core
{
    public interface IRepository<TEntity, TKey> where TEntity : IAppEntity<TKey>
    {
        void Add(TEntity entity);
        TEntity Get(TKey id);
        IQueryable<TEntity> Get();
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
