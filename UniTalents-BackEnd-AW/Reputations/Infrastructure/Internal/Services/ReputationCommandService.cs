using UniTalents_BackEnd_AW.Reputations.Application.Internal.Services;
using UniTalents_BackEnd_AW.Reputations.Domain.Entities;
using UniTalents_BackEnd_AW.Reputations.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Students.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Reputations.Infrastructure.Internal.Services;

public class ReputationCommandService : IReputationCommandService
{
    private readonly IReputationRepository _reputationRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProjectRepository _projectRepository;

    public ReputationCommandService(
        IReputationRepository reputationRepository,
        IStudentRepository studentRepository,
        IProjectRepository projectRepository)
    {
        _reputationRepository = reputationRepository;
        _studentRepository    = studentRepository;
        _projectRepository    = projectRepository;
    }

    /// <summary>
    /// Crea una reputación al finalizar el proyecto, actualiza el rating del estudiante,
    /// marca el proyecto como Finished y agrega el proyecto a los finalizados del estudiante.
    /// </summary>
    public async Task<Reputation> CreateAsync(int projectId, int rating, string comment)
    {
        // 1 – Verificar proyecto y estudiante seleccionado
        var project = await _projectRepository.FindByIdAsync(projectId)
                     ?? throw new Exception("Proyecto no encontrado.");

        if (project.StudentSelectedId is null)
            throw new Exception("El proyecto no tiene estudiante seleccionado.");

        var studentId = project.StudentSelectedId.Value;

        // 2 – Registrar la reputación
        var reputation = new Reputation(studentId, projectId, rating, comment);
        await _reputationRepository.AddAsync(reputation);

        // 3 – Actualizar rating y proyectos finalizados del estudiante
        var student = await _studentRepository.GetByIdAsync(studentId)
                     ?? throw new Exception("Estudiante no encontrado.");

        student.UpdateRating(rating);                    // Actualiza el rating
        student.AddEndedProject(projectId);              // Agrega el proyecto a finalizados
        _studentRepository.Update(student);              // Guardar cambios (no await porque es síncrono)

        // 4 – Marcar proyecto como Finished
        project.Finish();
        await _projectRepository.UpdateAsync(project);

        return reputation;
    }
}
