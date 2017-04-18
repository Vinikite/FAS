using System.Linq;
using System.Threading.Tasks;

namespace FAS.Core
{
    public interface IService<TEntity, TKey> where TEntity : class, IAppEntity<TKey>
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(TKey key);
        IQueryable<TEntity> Get();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey key);
    }
}
