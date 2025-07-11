using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;

namespace UniTalents_BackEnd_AW.Companies.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetAllAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }

    public async Task<List<Company>> GetByUserIdAsync(int userId)
    {
        return await _context.Companies
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Company> CreateAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task DeleteAsync(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company != null)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Company?> FindByUserIdAsync(int userId) =>
        await _context.Companies.FirstOrDefaultAsync(c => c.UserId == userId);
}