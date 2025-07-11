using UniTalents_BackEnd_AW.Projects.Domain.Enums;

namespace UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

public record ProjectDto(
    int Id,
    int CompanyId,
    string Title,
    string Description,
    string Field,
    decimal? Budget,
    List<string> Skills,
    List<int> Postulants,
    int? StudentSelected,
    bool IsFinished,
    ProjectStatus Status,
    DateTime CreatedAt
);