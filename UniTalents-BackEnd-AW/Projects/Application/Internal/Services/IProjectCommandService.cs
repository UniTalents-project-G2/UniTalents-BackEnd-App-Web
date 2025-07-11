using UniTalents_BackEnd_AW.Projects.Domain.Enums;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Projects.Application.Internal.Services;

public interface IProjectCommandService
{
    Task<ProjectDto> CreateAsync(CreateProjectRequest request, int companyId);
    Task<ProjectDto> UpdateAsync(int id, UpdateProjectRequest request, int companyId);
    Task<ProjectDto> AssignStudentAsync(int id, int studentId, int companyId);
    Task<ProjectDto> ChangeStatusAsync(int id, ProjectStatus status, int companyId);
    Task DeleteAsync(int id, int companyId);
}