using UniTalents_BackEnd_AW.Students.Application.Internal.Services;
using UniTalents_BackEnd_AW.Students.Domain.Entities;
using UniTalents_BackEnd_AW.Students.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Students.Infrastructure.Internal.Services;

public class StudentQueryService : IStudentQueryService
{
    private readonly IStudentRepository _studentRepository;

    public StudentQueryService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public async Task<Student?> GetByUserIdAsync(int userId)
    {
        return await _studentRepository.GetByUserIdAsync(userId);
    }
}