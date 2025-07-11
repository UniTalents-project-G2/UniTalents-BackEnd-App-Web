using UniTalents_BackEnd_AW.Reputations.Domain.Entities;

namespace UniTalents_BackEnd_AW.Reputations.Application.Internal.Services;

public interface IReputationCommandService
{
    Task<Reputation> CreateAsync(int projectId, int rating, string comment);
}