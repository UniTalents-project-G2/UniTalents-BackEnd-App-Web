using UniTalents_BackEnd_AW.Shared.Domain.Entities;

namespace UniTalents_BackEnd_AW.Companies.Domain.Entities;

/// <summary>
/// Una fila por (StudentId, ProjectId). Guarda la calificación de 1-5.
/// </summary>
public class CompanyRating : BaseEntity
{
    public int StudentId  { get; private set; }
    public int ProjectId  { get; private set; }
    public int Rating     { get; private set; }   // 1-5 inclusive

    // Constructor vacío para EF
    protected CompanyRating() { }

    public CompanyRating(int studentId, int projectId, int rating)
    {
        StudentId = studentId;
        ProjectId = projectId;
        Rating    = rating;
    }
    
    
    
    
}