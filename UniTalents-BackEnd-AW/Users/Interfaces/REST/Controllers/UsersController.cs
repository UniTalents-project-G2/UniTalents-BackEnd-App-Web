using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniTalents_BackEnd_AW.Users.Application.Internal;
using UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Users.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public UsersController(IUserCommandService commandService, IUserQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    // Registro
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var userDto = await _commandService.RegisterAsync(request);
        return Ok(userDto);
    }

    // Login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _commandService.LoginAsync(request);
        return Ok(response);
    }

    // Obtener todos
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var users = await _queryService.GetAllAsync();
        return Ok(users);
    }

    // Obtener por ID
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _queryService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // Eliminar
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _commandService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return Ok("Usuario eliminado correctamente.");
    }

    // Actualizar
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
    {
        var updatedUser = await _commandService.UpdateAsync(id, request);
        return Ok(updatedUser);
    }
}
