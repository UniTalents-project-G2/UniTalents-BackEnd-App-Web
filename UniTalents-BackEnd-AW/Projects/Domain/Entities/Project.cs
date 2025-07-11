using UniTalents_BackEnd_AW.Projects.Domain.Enums;
using UniTalents_BackEnd_AW.Shared.Domain.Entities;

namespace UniTalents_BackEnd_AW.Projects.Domain.Entities;

public class Project : BaseEntity
{
    public int CompanyId { get; private set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Field { get; set; } = null!;
    public decimal? Budget { get; set; }

    public List<string> Skills { get; set; } = new();
    public List<int> Postulants { get; private set; } = new();

    public int? StudentSelectedId { get; private set; }

    public ProjectStatus Status { get; set; }

    // Constructor EF
    private Project() { }

    // Constructor principal
    public Project(int companyId,
        string title,
        string description,
        string field,
        List<string> skills,
        decimal? budget,
        ProjectStatus status = ProjectStatus.Open) // se agregó
    {
        CompanyId = companyId;
        Title = title;
        Description = description;
        Field = field;
        Skills = skills;
        Budget = budget;
        Status = status; // ahora configurable
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }


    // Métodos de dominio
    public void UpdateDetails(string title, string description, string field, List<string> skills, decimal? budget, ProjectStatus status)
    {
        if (Status is ProjectStatus.Finished or ProjectStatus.Cancelled)
            throw new InvalidOperationException("No se puede editar un proyecto cerrado.");

        Title = title;
        Description = description;
        Field = field;
        Skills = skills;
        Budget = budget;
        Status = status;
    }


    public void AssignStudent(int studentId)
    {
        if (Status != ProjectStatus.Open)
            throw new InvalidOperationException("Solo proyectos abiertos pueden asignar estudiante.");

        StudentSelectedId = studentId;
        Status = ProjectStatus.InProgress;
    }

    public void Cancel()
    {
        if (Status == ProjectStatus.Finished)
            throw new InvalidOperationException("No se puede cancelar un proyecto finalizado.");

        Status = ProjectStatus.Cancelled;
    }


    public void AddPostulant(int studentId)
    {
        if (!Postulants.Contains(studentId))
        {
            Postulants.Add(studentId);
            // No cambia estado aún
        }
    }

    public void FinishWithStudent(int studentId)
    {
        if (!Postulants.Contains(studentId))
            throw new InvalidOperationException("El estudiante no está entre los postulantes.");

        StudentSelectedId = studentId;
        Status = ProjectStatus.InProgress; // valor 2
    }
    
    public void Finish()
    {
        Status = ProjectStatus.Finished; // valor 2 según tu enum
    }
}
