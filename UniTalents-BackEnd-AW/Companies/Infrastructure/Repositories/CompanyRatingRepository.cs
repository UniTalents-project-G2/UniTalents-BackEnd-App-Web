using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Repositories;

namespace UniTalents_BackEnd_AW.Companies.Infrastructure.Repositories;

public class CompanyRatingRepository : BaseRepository<CompanyRating>, ICompanyRatingRepository
{
    public CompanyRatingRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(int studentId, int projectId)
        => await Context.CompanyRatings
                        .AnyAsync(cr => cr.StudentId == studentId && cr.ProjectId == projectId);

    public async Task<IEnumerable<CompanyRating>> FindByCompanyIdAsync(int companyId)
    {
        // 1) Obtener los IDs de proyectos de esa compañía
        var projectIds = await Context.Projects
                                      .Where(p => p.CompanyId == companyId)
                                      .Select(p => p.Id)
                                      .ToListAsync();

        // 2) Filtrar las calificaciones que correspondan a esos proyectos
        return await Context.CompanyRatings
                            .Where(cr => projectIds.Contains(cr.ProjectId))
                            .ToListAsync();
    }
}
