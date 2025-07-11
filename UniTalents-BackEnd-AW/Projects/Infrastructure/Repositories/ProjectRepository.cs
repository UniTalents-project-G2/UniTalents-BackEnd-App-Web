using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Projects.Domain.Entities;
using UniTalents_BackEnd_AW.Projects.Domain.Enums;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Repositories;

namespace UniTalents_BackEnd_AW.Projects.Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context) { }

    public async Task<Project?> FindByIdAsync(int id)
    {
        return await Context.Projects.FindAsync(id);
    }

    public async Task<IEnumerable<Project>> ListAsync(ProjectQueryFilters f)
    {
        IQueryable<Project> q = Context.Projects.AsQueryable();

        if (f.CompanyId.HasValue)
            q = q.Where(p => p.CompanyId == f.CompanyId);

        if (f.Status.HasValue)
            q = q.Where(p => p.Status == f.Status);

        if (f.StudentSelectedId.HasValue)
            q = q.Where(p => p.StudentSelectedId == f.StudentSelectedId);

        if (f.IsFinished.HasValue)
        {
            q = q.Where(p =>
                f.IsFinished.Value
                    ? p.Status == ProjectStatus.Finished
                    : p.Status != ProjectStatus.Finished);
        }

        if (!string.IsNullOrWhiteSpace(f.Field))
            q = q.Where(p => p.Field == f.Field);

        return await q.ToListAsync();
    }
}