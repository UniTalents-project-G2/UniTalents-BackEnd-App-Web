using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;
using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;

namespace UniTalents_BackEnd_AW.Companies.Infrastructure.Internal.Services;

public class CompanyCommandService : ICompanyCommandService
{
    private readonly ICompanyRepository _repository;

    public CompanyCommandService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Company> CreateAsync(Company company)
    {
        return await _repository.CreateAsync(company);
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        return await _repository.UpdateAsync(company);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}