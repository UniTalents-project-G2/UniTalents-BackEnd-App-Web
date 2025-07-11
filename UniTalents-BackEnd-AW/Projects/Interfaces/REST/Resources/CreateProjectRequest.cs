using System.ComponentModel.DataAnnotations;
using UniTalents_BackEnd_AW.Projects.Domain.Enums;

namespace UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

public record CreateProjectRequest(
    [Required] string Title,
    [Required] string Description,
    [Required] string Field,
    [Required] List<string> Skills,
    [Required] decimal? Budget,
    [Required] ProjectStatus Status // ✅ Agregado
);