using UniTalents_BackEnd_AW.StudentPostulations.Application.Internal.Services;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Repositories;

namespace UniTalents_BackEnd_AW.StudentPostulations.Infrastructure.Internal.Services;

public class StudentPostulationQueryService : IStudentPostulationQueryService
{
    private readonly IStudentPostulationRepository _repository;

    public StudentPostulationQueryService(IStudentPostulationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudentPostulation>> GetByStudentIdAsync(int studentId)
    {
        return await _repository.FindByStudentIdAsync(studentId);
    }

    public async Task<IEnumerable<StudentPostulation>> GetByProjectIdAsync(int projectId)
    {
        return await _repository.FindByProjectIdAsync(projectId);
    }
}