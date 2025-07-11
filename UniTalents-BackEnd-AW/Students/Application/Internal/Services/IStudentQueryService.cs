using UniTalents_BackEnd_AW.Students.Domain.Entities;

namespace UniTalents_BackEnd_AW.Students.Application.Internal.Services;

public interface IStudentQueryService
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByUserIdAsync(int userId);
}