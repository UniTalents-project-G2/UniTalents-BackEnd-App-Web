using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Companies.Interfaces.REST.Transform;

public static class CompanyMapper
{
    public static CompanyDto ToResource(Company model) => new()
    {
        Id = model.Id,
        UserId = model.UserId,
        CompanyName = model.CompanyName,
        Sector = model.Sector,
        Location = model.Location,
        Email = model.Email,
        Phone = model.Phone,
        Rating = model.Rating,
        Specializations = model.Specializations,
        Logo = model.Logo,
        Description = model.Description
    };

    public static Company ToModel(CreateCompanyRequest request) => new()
    {
        UserId = request.UserId,
        CompanyName = request.CompanyName,
        Sector = request.Sector,
        Location = request.Location,
        Email = request.Email,
        Phone = request.Phone,
        Rating = 0,
        Specializations = request.Specializations,
        Logo = request.Logo,
        Description = request.Description
    };

    public static void MapUpdate(Company model, UpdateCompanyRequest request)
    {
        model.CompanyName = request.CompanyName;
        model.Sector = request.Sector;
        model.Location = request.Location;
        model.Email = request.Email;
        model.Phone = request.Phone;
        model.Specializations = request.Specializations;
        model.Logo = request.Logo;
        model.Description = request.Description;
    }
}