using FAS.Core;
using FAS.DAL.Repository;
using FAS.Domain;
using System;

namespace FAS.BLL
{
    public interface IScoreService : IService<Score, Guid> { }

    public class ScoreService : Service<Score, Guid>, IScoreService
    {
        public ScoreService(IAppRepository<Score> repo, IUnitOfWork uow) : base(repo, uow) { }
    }
}
