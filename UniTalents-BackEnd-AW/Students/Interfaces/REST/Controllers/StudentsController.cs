using Microsoft.AspNetCore.Mvc;
using UniTalents_BackEnd_AW.Students.Application.Internal.Services;
using UniTalents_BackEnd_AW.Students.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Students.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.Students.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(
    IStudentCommandService commandService,
    IStudentQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await queryService.GetAllAsync();
        var resources = students.Select(StudentMapper.ToDto);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await queryService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(StudentMapper.ToDto(student));
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var student = await queryService.GetByUserIdAsync(userId);
        if (student == null) return NotFound();
        return Ok(StudentMapper.ToDto(student));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
    {
        var student = StudentMapper.ToDomainModel(request);
        var created = await commandService.CreateAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, StudentMapper.ToDto(created));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentRequest request)
    {
        var updated = await commandService.UpdateAsync(id, request.City, request.Country,
            request.PhoneNumber, request.PortfolioLink, request.AboutMe, request.Logo,
            request.Specializations); // ✅ añadido

        if (updated == null) return NotFound();
        return Ok(StudentMapper.ToDto(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await commandService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
