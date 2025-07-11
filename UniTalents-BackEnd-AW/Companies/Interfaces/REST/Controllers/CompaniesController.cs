using Microsoft.AspNetCore.Mvc;
using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;
using UniTalents_BackEnd_AW.Companies.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Companies.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.Companies.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyCommandService _commandService;
    private readonly ICompanyQueryService _queryService;

    public CompaniesController(
        ICompanyCommandService commandService,
        ICompanyQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
    {
        var companies = await _queryService.GetAllAsync();
        return Ok(companies.Select(CompanyMapper.ToResource));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CompanyDto>> GetById(int id)
    {
        var company = await _queryService.GetByIdAsync(id);
        return company is null ? NotFound() : Ok(CompanyMapper.ToResource(company));
    }

    [HttpGet("by-user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetByUserId(int userId)
    {
        var companies = await _queryService.GetByUserIdAsync(userId);
        return Ok(companies.Select(CompanyMapper.ToResource));
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Create([FromBody] CreateCompanyRequest request)
    {
        var model = CompanyMapper.ToModel(request);
        var created = await _commandService.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, CompanyMapper.ToResource(created));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CompanyDto>> Update(int id, [FromBody] UpdateCompanyRequest request)
    {
        var existing = await _queryService.GetByIdAsync(id);
        if (existing is null) return NotFound();

        CompanyMapper.MapUpdate(existing, request);
        var updated = await _commandService.UpdateAsync(existing);
        return Ok(CompanyMapper.ToResource(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _queryService.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _commandService.DeleteAsync(id);
        return NoContent();
    }
}
