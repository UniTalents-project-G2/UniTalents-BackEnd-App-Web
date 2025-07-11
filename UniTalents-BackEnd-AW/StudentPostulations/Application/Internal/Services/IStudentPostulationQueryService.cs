using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;

namespace UniTalents_BackEnd_AW.StudentPostulations.Application.Internal.Services;

public interface IStudentPostulationQueryService
{
    Task<IEnumerable<StudentPostulation>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<StudentPostulation>> GetByProjectIdAsync(int projectId);
}