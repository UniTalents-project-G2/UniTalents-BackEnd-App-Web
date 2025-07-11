using UniTalents_BackEnd_AW.Shared.Domain.Entities;

namespace UniTalents_BackEnd_AW.Reputations.Domain.Entities;

public class Reputation : BaseEntity 
{
    public int Id { get; private set; }
    public int StudentId { get; set; }
    public int ProjectId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;

    public Reputation() { }

    public Reputation(int studentId, int projectId, int rating, string comment)
    {
        StudentId = studentId;
        ProjectId = projectId;
        Rating = rating;
        Comment = comment;
    }
}