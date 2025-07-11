using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Projects.Application.Internal.Services;

public interface IProjectQueryService
{
    Task<ProjectDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProjectDto>> GetAllAsync(ProjectQueryFilters filters);
}