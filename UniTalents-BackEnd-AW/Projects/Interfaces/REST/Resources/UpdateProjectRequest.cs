using System.ComponentModel.DataAnnotations;
using UniTalents_BackEnd_AW.Projects.Domain.Enums;

namespace UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

public record UpdateProjectRequest(
    [Required] string Title,
    [Required] string Description,
    [Required] string Field,
    List<string> Skills,
    decimal? Budget,
    [Required] ProjectStatus Status // ✅ Agregado
);