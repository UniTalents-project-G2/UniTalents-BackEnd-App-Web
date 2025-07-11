using UniTalents_BackEnd_AW.Reputations.Domain.Entities;

namespace UniTalents_BackEnd_AW.Reputations.Application.Internal.Services;

public interface IReputationQueryService
{
    Task<IEnumerable<Reputation>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<Reputation>> GetByProjectIdAsync(int projectId);
}