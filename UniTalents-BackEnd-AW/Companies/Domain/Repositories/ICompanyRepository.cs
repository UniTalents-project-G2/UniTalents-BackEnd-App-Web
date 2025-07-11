using UniTalents_BackEnd_AW.Companies.Domain.Entities;

namespace UniTalents_BackEnd_AW.Companies.Domain.Repositories;
public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<List<Company>> GetByUserIdAsync(int userId);
    Task<Company> CreateAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task DeleteAsync(int id);
    Task<Company?> FindByUserIdAsync(int userId);
}