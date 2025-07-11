using UniTalents_BackEnd_AW.Students.Domain.Entities;
using UniTalents_BackEnd_AW.Students.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Students.Interfaces.REST.Transform;

public static class StudentMapper
{
    public static StudentDto ToDto(Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            UserId = student.UserId,
            Birthdate = student.Birthdate,
            City = student.City,
            Country = student.Country,
            Field = student.Field,
            PhoneNumber = student.PhoneNumber,
            PortfolioLink = student.PortfolioLink,
            AboutMe = student.AboutMe,
            Rating = student.Rating,
            Specializations = student.Specializations,
            Logo = student.Logo,
            EndedProjects = student.EndedProjects
        };
    }

    public static Student ToDomainModel(CreateStudentRequest request)
    {
        return new Student(
            request.UserId,
            request.Birthdate,
            request.City,
            request.Country,
            request.Field,
            request.PhoneNumber,
            request.PortfolioLink,
            request.AboutMe,
            request.Rating,
            request.Specializations ?? new List<string>(),
            request.Logo,
            request.EndedProjects ?? new List<int>()
        );
    }
}