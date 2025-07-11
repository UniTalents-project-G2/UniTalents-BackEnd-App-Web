namespace UniTalents_BackEnd_AW.StudentPostulations.Interfaces.REST.Resources;

public class StudentPostulationDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ProjectId { get; set; }
    public string Status { get; set; } = null!;
    public string Date { get; set; } = null!;
}