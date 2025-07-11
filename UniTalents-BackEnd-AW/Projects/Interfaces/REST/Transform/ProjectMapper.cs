using UniTalents_BackEnd_AW.Projects.Domain.Entities;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Projects.Interfaces.REST.Transform;

public static class ProjectMapper
{
    // Convertir entidad a DTO
    public static ProjectDto ToResource(Project model) =>
        new(
            model.Id,
            model.CompanyId,
            model.Title,
            model.Description,
            model.Field,
            model.Budget,
            model.Skills,
            model.Postulants,
            model.StudentSelectedId,
            model.Status == Domain.Enums.ProjectStatus.Finished,
            model.Status,
            model.CreatedAt
        );

    // Mapear actualización desde request a entidad
    public static void MapUpdate(Project project, UpdateProjectRequest request)
    {
        project.Title = request.Title;
        project.Description = request.Description;
        project.Field = request.Field;
        project.Skills = request.Skills;
        project.Budget = request.Budget;
        project.Status = request.Status; // ✅ actualización del estado
    }
}