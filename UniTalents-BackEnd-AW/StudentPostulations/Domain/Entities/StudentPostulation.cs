using UniTalents_BackEnd_AW.Shared.Domain.Entities;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Enums;

namespace UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;

public class StudentPostulation : BaseEntity
{
    public int StudentId { get; private set; }
    public int ProjectId { get; private set; }
    public PostulationStatus Status { get; private set; }
    public DateTime Date { get; private set; }

    // Constructor vacío para EF Core
    private StudentPostulation() { }

    public StudentPostulation(int studentId, int projectId)
    {
        StudentId = studentId;
        ProjectId = projectId;
        Status = PostulationStatus.Sent;
        Date = DateTime.UtcNow.Date;

    }

    public void Accept() => Status = PostulationStatus.Accepted;
    public void Reject() => Status = PostulationStatus.Rejected;
}