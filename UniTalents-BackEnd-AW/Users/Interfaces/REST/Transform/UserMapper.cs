using UniTalents_BackEnd_AW.Users.Domain.Entities;
using UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Users.Interfaces.REST.Transform;

public static class UserMapper
{
    public static UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}