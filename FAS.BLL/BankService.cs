using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;

namespace FAS.BLL
{
    public interface IBankService : IService<Bank, Guid> { }

    public class BankService : Service<Bank, Guid>, IBankService
    {
        public BankService(IAppRepository<Bank> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
