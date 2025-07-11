using UniTalents_BackEnd_AW.Projects.Application.Internal.Services;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.Projects.Infrastructure.Internal.Services;

public class ProjectQueryService : IProjectQueryService
{
    private readonly IProjectRepository _repository;

    public ProjectQueryService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await _repository.FindByIdAsync(id);
        return project is null ? null : ProjectMapper.ToResource(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync(ProjectQueryFilters filters)
    {
        var projects = await _repository.ListAsync(filters);
        return projects.Select(ProjectMapper.ToResource);
    }
}