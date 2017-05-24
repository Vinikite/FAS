using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;


namespace FAS.BLL
{
    public interface ITransactionTypeService : IService<TransactionType, Guid> { }

    public class TransactionTypeService : Core.Service<TransactionType, Guid>, ITransactionTypeService
    {
        public TransactionTypeService(IAppRepository<TransactionType> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
