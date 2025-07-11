using UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Users.Domain.Entities;

namespace UniTalents_BackEnd_AW.Users.Application.Internal;

public interface IUserCommandService
{
    Task<UserDto> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    //Task<UserDto> UpdateAsync(int id, RegisterRequest request); // o UpdateUserRequest si lo separas luego
    Task<bool> DeleteAsync(int id);
    
    Task<UserDto> UpdateAsync(int id, UpdateUserRequest request);
    
    

}