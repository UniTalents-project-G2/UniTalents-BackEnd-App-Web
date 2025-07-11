namespace UniTalents_BackEnd_AW.Companies.Domain.Entities;

public class Company
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Sector { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public double Rating { get; set; } = 5;
    public List<string> Specializations { get; set; } = new();
    public string Logo { get; set; } = null!;
    public string Description { get; set; } = null!;
    
   
    public Company()
    {
        CompanyName = string.Empty;
        Sector = string.Empty;
        Location = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Rating = 0;
        Specializations = new();
        Logo = string.Empty;
        Description = string.Empty;
    }
    
    // Companies/Domain/Entities/Company.cs
    public void UpdateRating(int newRating)
    {
        Rating = Rating == 0
            ? newRating
            : (Rating + newRating) / 2.0;
    }


}