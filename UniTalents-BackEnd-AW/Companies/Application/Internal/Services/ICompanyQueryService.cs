using UniTalents_BackEnd_AW.Companies.Domain.Entities;

namespace UniTalents_BackEnd_AW.Companies.Application.Internal.Services;

public interface ICompanyQueryService
{
    Task<List<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<List<Company>> GetByUserIdAsync(int userId);
}