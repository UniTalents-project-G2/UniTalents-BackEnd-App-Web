using UniTalents_BackEnd_AW.Companies.Domain.Entities;

namespace UniTalents_BackEnd_AW.Companies.Application.Internal.Services;

public interface ICompanyCommandService
{
    Task<Company> CreateAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task DeleteAsync(int id);
}