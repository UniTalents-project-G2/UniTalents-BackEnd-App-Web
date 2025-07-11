using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;
using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Companies.Infrastructure.Internal.Services;

public class CompanyQueryService : ICompanyQueryService
{
    private readonly ICompanyRepository _repository;

    public CompanyQueryService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Company>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Company>> GetByUserIdAsync(int userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }
}