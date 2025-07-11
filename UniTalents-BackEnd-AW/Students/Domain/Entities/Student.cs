using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace UniTalents_BackEnd_AW.Students.Domain.Entities;

public class Student
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public DateTime Birthdate { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public string Field { get; private set; }
    public string PhoneNumber { get; private set; }
    public string PortfolioLink { get; private set; }
    public string AboutMe { get; private set; }
    public double Rating { get; private set; }
    public string Logo { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Backing fields serializados en BD
    public string SpecializationsJson { get; private set; } = "[]";
    public string EndedProjectsJson { get; private set; } = "[]";

    // ⚠️ Ignorados por EF, usados solo en código
    [NotMapped]
    public List<string> Specializations
    {
        get => JsonSerializer.Deserialize<List<string>>(SpecializationsJson) ?? new();
        private set => SpecializationsJson = JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public List<int> EndedProjects
    {
        get => JsonSerializer.Deserialize<List<int>>(EndedProjectsJson) ?? new();
        private set => EndedProjectsJson = JsonSerializer.Serialize(value);
    }

    protected Student() { }

    public Student(int userId, DateTime birthdate, string city, string country, string field,
        string phoneNumber, string portfolioLink, string aboutMe, double rating,
        List<string> specializations, string logo, List<int> endedProjects)
    {
        UserId = userId;
        Birthdate = birthdate;
        City = city;
        Country = country;
        Field = field;
        PhoneNumber = phoneNumber;
        PortfolioLink = portfolioLink;
        AboutMe = aboutMe;
        Rating = rating;
        Logo = logo;

        Specializations = specializations;
        EndedProjects = endedProjects;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string city, string country, string phoneNumber,
        string portfolioLink, string aboutMe, string logo, List<string> specializations)
    {
        City = city;
        Country = country;
        PhoneNumber = phoneNumber;
        PortfolioLink = portfolioLink;
        AboutMe = aboutMe;
        Logo = logo;
        Specializations = specializations;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateRating(int newRating)
    {
        // Si ya guardas la cantidad de reseñas podrías refinarlo; por simplicidad:
        if (Rating == 0)
            Rating = newRating;
        else
            Rating = (Rating + newRating) / 2;
    }
    
    public void AddEndedProject(int projectId)
    {
        var endedProjects = EndedProjects;

        if (!endedProjects.Contains(projectId))
        {
            endedProjects.Add(projectId);
            EndedProjects = endedProjects; // esto vuelve a serializar y guarda en EndedProjectsJson
            UpdatedAt = DateTime.UtcNow;
        }
    }

    
    


}
