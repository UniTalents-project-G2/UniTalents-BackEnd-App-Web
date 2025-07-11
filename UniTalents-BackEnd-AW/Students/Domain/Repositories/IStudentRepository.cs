using UniTalents_BackEnd_AW.Students.Domain.Entities;

namespace UniTalents_BackEnd_AW.Students.Domain.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByUserIdAsync(int userId);
    Task AddAsync(Student student);
    void Update(Student student);
    void Delete(Student student);
}