using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Repositories;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Repositories;

namespace UniTalents_BackEnd_AW.StudentPostulations.Infrastructure.Repositories;

public class StudentPostulationRepository : BaseRepository<StudentPostulation>, IStudentPostulationRepository
{
    public StudentPostulationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<StudentPostulation>> FindByStudentIdAsync(int studentId)
    {
        return await Context.Set<StudentPostulation>()
            .Where(p => p.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<StudentPostulation>> FindByProjectIdAsync(int projectId)
    {
        return await Context.Set<StudentPostulation>()
            .Where(p => p.ProjectId == projectId)
            .ToListAsync();
    }
}