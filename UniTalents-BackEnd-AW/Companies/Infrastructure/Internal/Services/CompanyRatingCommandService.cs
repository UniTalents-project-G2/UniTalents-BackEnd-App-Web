using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;
using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Companies.Infrastructure.Internal.Services;

public class CompanyRatingCommandService : ICompanyRatingCommandService
{
    private readonly ICompanyRatingRepository _ratingRepo;
    private readonly ICompanyRepository       _companyRepo;
    private readonly IProjectRepository       _projectRepo;

    public CompanyRatingCommandService(
        ICompanyRatingRepository ratingRepo,
        ICompanyRepository       companyRepo,
        IProjectRepository       projectRepo)
    {
        _ratingRepo  = ratingRepo;
        _companyRepo = companyRepo;
        _projectRepo = projectRepo;
    }

    public async Task<CompanyRating> CreateAsync(int projectId, int rating, int studentId)
    {
        var project = await _projectRepo.FindByIdAsync(projectId)
                     ?? throw new Exception("Proyecto no encontrado.");

        if (project.Status != Projects.Domain.Enums.ProjectStatus.Finished)
            throw new Exception("El proyecto aún no está finalizado.");

        if (project.StudentSelectedId != studentId)
            throw new Exception("Este estudiante no pertenece a este proyecto.");
        
        if (await _ratingRepo.ExistsAsync(studentId, projectId))
            throw new Exception("Ya calificaste esta empresa para este proyecto.");
        
        var company = await _companyRepo.GetByIdAsync(project.CompanyId)
                     ?? throw new Exception("Compañía no encontrada.");
        
        var ratingRow = new CompanyRating(studentId, projectId, rating);
        await _ratingRepo.AddAsync(ratingRow);
        
        company.UpdateRating(rating);
        await _companyRepo.UpdateAsync(company);

        return ratingRow;
    }
}
