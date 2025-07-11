using UniTalents_BackEnd_AW.Reputations.Domain.Entities;
using UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Transform;

public static class ReputationMapper
{
    public static ReputationDto ToDto(Reputation r) => new ReputationDto
    {
        Id        = r.Id,
        StudentId = r.StudentId,
        ProjectId = r.ProjectId,
        Rating    = r.Rating,
        Comment   = r.Comment
    };
}