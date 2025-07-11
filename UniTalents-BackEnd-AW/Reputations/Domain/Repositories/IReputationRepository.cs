using UniTalents_BackEnd_AW.Reputations.Domain.Entities;
using UniTalents_BackEnd_AW.Shared.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Reputations.Domain.Repositories;

public interface IReputationRepository : IBaseRepository<Reputation>
{
    Task<IEnumerable<Reputation>> FindByStudentIdAsync(int studentId);
    Task<IEnumerable<Reputation>> FindByProjectIdAsync(int projectId);
}