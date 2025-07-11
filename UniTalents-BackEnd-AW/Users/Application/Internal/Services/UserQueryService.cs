using UniTalents_BackEnd_AW.Users.Application.Internal;
using UniTalents_BackEnd_AW.Users.Domain.Repositories;
using UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Users.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.Users.Application.Internal.Services;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _repository;

    public UserQueryService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return users.Select(UserMapper.ToDto);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user != null ? UserMapper.ToDto(user) : null;
    }

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        var user = await _repository.GetByEmailAsync(email);
        return user != null ? UserMapper.ToDto(user) : null;
    }
}