namespace UniTalents_BackEnd_AW.Companies.Interfaces.REST.Resources;

public class CompanyDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Sector { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public double Rating { get; set; }
    public List<string> Specializations { get; set; } = new();
    public string Logo { get; set; } = null!;
    public string Description { get; set; } = null!;
}