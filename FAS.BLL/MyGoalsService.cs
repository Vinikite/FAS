using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;

namespace FAS.BLL
{
    public interface IMyGoalsService : IService<MyGoals, Guid> { }

    public class MyGoalsService : Service<MyGoals, Guid>, IMyGoalsService
    {
        public MyGoalsService(IAppRepository<MyGoals> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
