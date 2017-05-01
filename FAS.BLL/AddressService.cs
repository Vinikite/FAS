using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;

namespace FAS.BLL
{
    public interface IAddressService : IService<Address, Guid> { }

    public class AddressService : Service<Address, Guid>, IAddressService
    {
        public AddressService(IAppRepository<Address> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
