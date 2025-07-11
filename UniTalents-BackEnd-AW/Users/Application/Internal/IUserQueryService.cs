using UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Users.Application.Internal;


public interface IUserQueryService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto?> GetByEmailAsync(string email);
}