using UniTalents_BackEnd_AW.Projects.Domain.Entities;
using UniTalents_BackEnd_AW.Shared.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Projects.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<Project?> FindByIdAsync(int id); // especializado
    Task<IEnumerable<Project>> ListAsync(ProjectQueryFilters filters);
    
    
}