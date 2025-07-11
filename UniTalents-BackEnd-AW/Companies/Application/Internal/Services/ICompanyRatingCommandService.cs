using UniTalents_BackEnd_AW.Companies.Domain.Entities;

namespace UniTalents_BackEnd_AW.Companies.Application.Internal.Services;

public interface ICompanyRatingCommandService
{
    Task<CompanyRating> CreateAsync(int projectId, int rating, int studentId);
}