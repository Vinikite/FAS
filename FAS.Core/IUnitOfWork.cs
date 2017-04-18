using System.Threading.Tasks;

namespace FAS.Core
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
