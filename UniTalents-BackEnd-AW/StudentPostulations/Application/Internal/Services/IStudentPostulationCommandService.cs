using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;

namespace UniTalents_BackEnd_AW.StudentPostulations.Application.Internal.Services;

public interface IStudentPostulationCommandService
{
    Task<StudentPostulation> CreateAsync(int studentId, int projectId);
    Task<StudentPostulation> AcceptAsync(int postulationId);
}