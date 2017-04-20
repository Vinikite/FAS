using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;

namespace FAS.BLL
{
    public interface ITransactionService : IService<Transaction, Guid> { }

    public class TransactionService : Service<Transaction, Guid>, ITransactionService
    {
        public TransactionService(IAppRepository<Transaction> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
