using UniTalents_BackEnd_AW.Projects.Application.Internal.Services;
using UniTalents_BackEnd_AW.Projects.Domain.Entities;
using UniTalents_BackEnd_AW.Projects.Domain.Enums;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.Projects.Infrastructure.Internal.Services;

public class ProjectCommandService : IProjectCommandService
{
    private readonly IProjectRepository _repository;

    public ProjectCommandService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectRequest request, int companyId)
    {
        var project = new Project(
            companyId,
            request.Title,
            request.Description,
            request.Field,
            request.Skills ?? new List<string>(),
            request.Budget,
            request.Status // ahora configurable desde el request
        );

        await _repository.AddAsync(project);
        return ProjectMapper.ToResource(project);
    }

    public async Task<ProjectDto> UpdateAsync(int id, UpdateProjectRequest request, int companyId)
    {
        var project = await _repository.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("Proyecto no encontrado");

        if (project.CompanyId != companyId)
            throw new UnauthorizedAccessException("No puedes editar este proyecto");

        project.UpdateDetails(
            request.Title,
            request.Description,
            request.Field,
            request.Skills,
            request.Budget,
            request.Status // ahora configurable desde el request
        );

        await _repository.UpdateAsync(project);
        return ProjectMapper.ToResource(project);
    }

    public async Task<ProjectDto> AssignStudentAsync(int id, int studentId, int companyId)
    {
        var project = await _repository.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("Proyecto no encontrado");

        if (project.CompanyId != companyId)
            throw new UnauthorizedAccessException("No puedes modificar este proyecto");

        project.AssignStudent(studentId);

        await _repository.UpdateAsync(project);
        return ProjectMapper.ToResource(project);
    }

    public async Task<ProjectDto> ChangeStatusAsync(int id, ProjectStatus status, int companyId)
    {
        var project = await _repository.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("Proyecto no encontrado");

        if (project.CompanyId != companyId)
            throw new UnauthorizedAccessException("No puedes modificar este proyecto");

        switch (status)
        {
            case ProjectStatus.Finished:
                project.Finish();
                break;
            case ProjectStatus.Cancelled:
                project.Cancel();
                break;
            default:
                throw new InvalidOperationException("El estado solo puede cambiar a Finalizado o Cancelado");
        }

        await _repository.UpdateAsync(project);
        return ProjectMapper.ToResource(project);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        var project = await _repository.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("Proyecto no encontrado");

        if (project.CompanyId != companyId)
            throw new UnauthorizedAccessException("No puedes eliminar este proyecto");

        await _repository.RemoveAsync(project);
    }
}
