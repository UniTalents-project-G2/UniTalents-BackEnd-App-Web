using UniTalents_BackEnd_AW.Reputations.Application.Internal.Services;
using UniTalents_BackEnd_AW.Reputations.Domain.Entities;
using UniTalents_BackEnd_AW.Reputations.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Reputations.Infrastructure.Internal.Services;

public class ReputationQueryService : IReputationQueryService
{
    private readonly IReputationRepository _reputationRepository;

    public ReputationQueryService(IReputationRepository reputationRepository)
    {
        _reputationRepository = reputationRepository;
    }

    public async Task<IEnumerable<Reputation>> GetByStudentIdAsync(int studentId)
    {
        return await _reputationRepository.FindByStudentIdAsync(studentId);
    }

    public async Task<IEnumerable<Reputation>> GetByProjectIdAsync(int projectId)
    {
        return await _reputationRepository.FindByProjectIdAsync(projectId);
    }
}