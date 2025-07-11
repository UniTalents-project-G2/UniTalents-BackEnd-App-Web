using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;
using UniTalents_BackEnd_AW.Shared.Domain.Repositories;

namespace UniTalents_BackEnd_AW.StudentPostulations.Domain.Repositories;

public interface IStudentPostulationRepository : IBaseRepository<StudentPostulation>
{
    Task<IEnumerable<StudentPostulation>> FindByStudentIdAsync(int studentId);
    Task<IEnumerable<StudentPostulation>> FindByProjectIdAsync(int projectId);
}