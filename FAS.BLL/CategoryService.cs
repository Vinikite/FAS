using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;

namespace FAS.BLL
{
    public interface ICategoryService : IService<Category, Guid> { }

    public class CategoryService : Service<Category, Guid>, ICategoryService
    {
        public CategoryService(IAppRepository<Category> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
