namespace UniTalents_BackEnd_AW.Students.Interfaces.REST.Resources;

public class CreateStudentRequest
{
    public int UserId { get; set; }
    public DateTime Birthdate { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Field { get; set; }
    public string PhoneNumber { get; set; }
    public string PortfolioLink { get; set; }
    public string AboutMe { get; set; }
    public double Rating { get; set; }
    public List<string> Specializations { get; set; }
    public string Logo { get; set; }
    public List<int> EndedProjects { get; set; }
}