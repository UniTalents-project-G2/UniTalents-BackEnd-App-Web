using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Shared.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Companies.Domain.Repositories;

public interface ICompanyRatingRepository : IBaseRepository<CompanyRating>
{
    /// <summary>Devuelve true si ya existe una calificación para el par (studentId, projectId).</summary>
    Task<bool> ExistsAsync(int studentId, int projectId);

    Task<IEnumerable<CompanyRating>> FindByCompanyIdAsync(int companyId);
}