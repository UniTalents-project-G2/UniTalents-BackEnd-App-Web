using UniTalents_BackEnd_AW.Students.Domain.Entities;

namespace UniTalents_BackEnd_AW.Students.Application.Internal.Services;

public interface IStudentCommandService
{
    Task<Student?> CreateAsync(Student student);

    Task<Student?> UpdateAsync(int id, string city, string country, string phoneNumber,
        string portfolioLink, string aboutMe, string logo, List<string> specializations);

    Task<bool> DeleteAsync(int id);
}