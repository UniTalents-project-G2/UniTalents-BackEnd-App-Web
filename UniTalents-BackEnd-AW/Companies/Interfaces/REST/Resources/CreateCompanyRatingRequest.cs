namespace UniTalents_BackEnd_AW.Companies.Interfaces.REST.Resources;

public class CreateCompanyRatingRequest
{
    public int ProjectId { get; set; }
    public int Rating    { get; set; }   // 1 – 5
}