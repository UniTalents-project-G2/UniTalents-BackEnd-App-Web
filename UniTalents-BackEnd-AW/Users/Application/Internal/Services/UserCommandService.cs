using BCrypt.Net;
using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Companies.Application.Internal;
using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;
using UniTalents_BackEnd_AW.Users.Application.Internal;
using UniTalents_BackEnd_AW.Users.Domain.Entities;
using UniTalents_BackEnd_AW.Users.Domain.Repositories;
using UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Users.Interfaces.REST.Transform;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Security;
using UniTalents_BackEnd_AW.Students.Domain.Entities;
using UniTalents_BackEnd_AW.Students.Application.Internal;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Students.Application.Internal.Services;

namespace UniTalents_BackEnd_AW.Users.Application.Internal.Services;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;
    private readonly IStudentCommandService _studentCommandService;
    private readonly ICompanyCommandService _companyCommandService;
    private readonly AppDbContext _context;

    public UserCommandService(
        IUserRepository repository,
        ITokenService tokenService,
        IStudentCommandService studentCommandService,
        ICompanyCommandService companyCommandService,
        AppDbContext context)
    {
        _repository = repository;
        _tokenService = tokenService;
        _studentCommandService = studentCommandService;
        _companyCommandService = companyCommandService;
        _context = context;
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest request)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.Name, request.Email, passwordHash, request.Role);

        await _repository.AddAsync(user);
        await _context.SaveChangesAsync();

        return UserMapper.ToDto(user);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repository.GetByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas");

        var token = _tokenService.GenerateToken(user);
        return new LoginResponse
        {
            Token = token,
            User = UserMapper.ToDto(user)
        };
    }

    public async Task<UserDto> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

        user.Update(request.Name, request.Email, user.Role); // mantenemos el rol actual
        await _repository.UpdateAsync(user);
        return UserMapper.ToDto(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return false;

        await _repository.DeleteAsync(user);
        return true;
    }
}
