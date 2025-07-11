using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.StudentPostulations.Application.Internal.Services;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Repositories;

namespace UniTalents_BackEnd_AW.StudentPostulations.Infrastructure.Internal.Services;

public class StudentPostulationCommandService : IStudentPostulationCommandService
{
    private readonly IStudentPostulationRepository _postulationRepository;
    private readonly IProjectRepository _projectRepository;

    public StudentPostulationCommandService(
        IStudentPostulationRepository postulationRepository,
        IProjectRepository projectRepository)
    {
        _postulationRepository = postulationRepository;
        _projectRepository = projectRepository;
    }

    public async Task<StudentPostulation> CreateAsync(int studentId, int projectId)
    {
        var postulation = new StudentPostulation(studentId, projectId);

        var project = await _projectRepository.FindByIdAsync(projectId);
        project!.AddPostulant(studentId);

        await _projectRepository.UpdateAsync(project);
        await _postulationRepository.AddAsync(postulation);

        return postulation;
    }

    public async Task<StudentPostulation> AcceptAsync(int postulationId)
    {
        var postulation = await _postulationRepository.FindByIdAsync(postulationId)
            ?? throw new Exception("Postulación no encontrada");

        var allPostulations = await _postulationRepository.FindByProjectIdAsync(postulation.ProjectId);

        foreach (var p in allPostulations)
        {
            if (p.Id == postulationId)
                p.Accept();
            else
                p.Reject();

            await _postulationRepository.UpdateAsync(p);
        }

        var project = await _projectRepository.FindByIdAsync(postulation.ProjectId);
        project!.FinishWithStudent(postulation.StudentId);

        await _projectRepository.UpdateAsync(project);

        return postulation;
    }
}
