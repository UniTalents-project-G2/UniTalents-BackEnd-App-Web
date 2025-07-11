using UniTalents_BackEnd_AW.Students.Application.Internal.Services;
using UniTalents_BackEnd_AW.Students.Domain.Entities;
using UniTalents_BackEnd_AW.Students.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Students.Infrastructure.Internal.Services;

public class StudentCommandService : IStudentCommandService
{
    private readonly IStudentRepository _studentRepository;

    public StudentCommandService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> CreateAsync(Student student)
    {
        await _studentRepository.AddAsync(student);
        return student;
    }

    public async Task<Student?> UpdateAsync(int id, string city, string country, string phoneNumber,
        string portfolioLink, string aboutMe, string logo, List<string> specializations)
    {
        var existing = await _studentRepository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.Update(city, country, phoneNumber, portfolioLink, aboutMe, logo, specializations);
        _studentRepository.Update(existing);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _studentRepository.GetByIdAsync(id);
        if (existing == null) return false;

        _studentRepository.Delete(existing);
        return true;
    }
}