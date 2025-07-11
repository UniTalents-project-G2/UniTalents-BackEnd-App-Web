using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;          // 👈 nuevo
using UniTalents_BackEnd_AW.Projects.Application.Internal.Services;
using UniTalents_BackEnd_AW.Projects.Domain.Enums;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Interfaces.REST.Resources;

namespace UniTalents_BackEnd_AW.Projects.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectCommandService _command;
    private readonly IProjectQueryService  _query;
    private readonly ICompanyRepository    _companyRepository;     // 👈 nuevo

    public ProjectsController(
        IProjectCommandService command,
        IProjectQueryService  query,
        ICompanyRepository    companyRepository)                  // 👈 nuevo
    {
        _command           = command;
        _query             = query;
        _companyRepository = companyRepository;                   // 👈 nuevo
    }

    // 🔒 Devuelve el companyId asociado al usuario autenticado
    private async Task<int> GetCompanyIdAsync()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                               ?? throw new InvalidOperationException("No se encontró el ID del usuario en el token."));

        var company = await _companyRepository.FindByUserIdAsync(userId);
        if (company is null)
            throw new InvalidOperationException("El usuario no tiene una compañía asociada.");

        return company.Id;
    }
    

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProjectQueryFilters filters)
    {
        var result = await _query.GetAllAsync(filters);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _query.GetByIdAsync(id);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    [Authorize(Roles = "company")]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (request.Skills is null || !request.Skills.Any())
            return BadRequest("El proyecto debe incluir al menos una skill.");
        if (request.Budget is null || request.Budget <= 0)
            return BadRequest("El presupuesto es obligatorio y debe ser mayor a cero.");

        int companyId = await GetCompanyIdAsync();                 // 👈 usa companyId correcto

        var created = await _command.CreateAsync(request, companyId);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "company")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProjectRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int companyId = await GetCompanyIdAsync();                 // 👈 usa companyId correcto

        var updated = await _command.UpdateAsync(id, request, companyId);
        return Ok(updated);
    }

    [HttpPut("{id:int}/assign-student/{studentId:int}")]
    [Authorize(Roles = "company")]
    public async Task<IActionResult> AssignStudent(int id, int studentId)
    {
        int companyId = await GetCompanyIdAsync();                 // 👈 usa companyId correcto

        var updated = await _command.AssignStudentAsync(id, studentId, companyId);
        return Ok(updated);
    }

    [HttpPut("{id:int}/status/{status}")]
    [Authorize(Roles = "company")]
    public async Task<IActionResult> ChangeStatus(int id, ProjectStatus status)
    {
        int companyId = await GetCompanyIdAsync();                 // 👈 usa companyId correcto

        var updated = await _command.ChangeStatusAsync(id, status, companyId);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "company")]
    public async Task<IActionResult> Delete(int id)
    {
        int companyId = await GetCompanyIdAsync();                 // 👈 usa companyId correcto

        await _command.DeleteAsync(id, companyId);
        return NoContent();
    }
}
