using UniTalents_BackEnd_AW.Projects.Domain.Enums;

namespace UniTalents_BackEnd_AW.Projects.Domain.Repositories;

public class ProjectQueryFilters
{
    public int? CompanyId { get; set; }
    public ProjectStatus? Status { get; set; }
    public int? StudentSelectedId { get; set; }
    public bool? IsFinished { get; set; }
    public string? Field { get; set; }
}