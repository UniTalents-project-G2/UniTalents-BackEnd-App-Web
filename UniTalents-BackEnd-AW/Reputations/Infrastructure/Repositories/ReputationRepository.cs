using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Reputations.Domain.Entities;
using UniTalents_BackEnd_AW.Reputations.Domain.Repositories;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Repositories;

namespace UniTalents_BackEnd_AW.Reputations.Infrastructure.Repositories;

public class ReputationRepository : BaseRepository<Reputation>, IReputationRepository
{
    public ReputationRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Reputation>> FindByStudentIdAsync(int studentId)
    {
        return await Context.Set<Reputation>()
            .Where(r => r.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reputation>> FindByProjectIdAsync(int projectId)
    {
        return await Context.Set<Reputation>()
            .Where(r => r.ProjectId == projectId)
            .ToListAsync();
    }
}