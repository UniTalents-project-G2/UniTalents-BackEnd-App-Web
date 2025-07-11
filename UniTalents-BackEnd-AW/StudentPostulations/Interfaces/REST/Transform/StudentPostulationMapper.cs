using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Enums;
using UniTalents_BackEnd_AW.StudentPostulations.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.StudentPostulations.Interfaces.REST.Transform;

public static class StudentPostulationMapper
{
    public static StudentPostulationDto ToDto(StudentPostulation postulation)
    {
        var status = postulation.Status switch
        {
            PostulationStatus.Sent => "enviado",
            PostulationStatus.Accepted => "aceptado",
            PostulationStatus.Rejected => "rechazado",
            _ => "desconocido"
        };

        return new StudentPostulationDto
        {
            Id = postulation.Id,
            StudentId = postulation.StudentId,
            ProjectId = postulation.ProjectId,
            Status = status,
            Date = postulation.Date.ToString("yyyy-MM-dd")
        };
    }
}