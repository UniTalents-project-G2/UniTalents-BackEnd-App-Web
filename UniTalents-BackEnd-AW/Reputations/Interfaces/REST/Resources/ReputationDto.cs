namespace UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Resources;

public class ReputationDto
{
    public int Id         { get; set; }
    public int StudentId  { get; set; }
    public int ProjectId  { get; set; }
    public int Rating     { get; set; }
    public string Comment { get; set; } = string.Empty;
}