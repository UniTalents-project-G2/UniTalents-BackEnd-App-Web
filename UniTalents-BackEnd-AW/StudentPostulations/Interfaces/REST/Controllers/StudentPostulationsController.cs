using Microsoft.AspNetCore.Mvc;
using UniTalents_BackEnd_AW.StudentPostulations.Application.Internal.Services;
using UniTalents_BackEnd_AW.StudentPostulations.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.StudentPostulations.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.StudentPostulations.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentPostulationsController : ControllerBase
{
    private readonly IStudentPostulationCommandService _commandService;
    private readonly IStudentPostulationQueryService _queryService;

    public StudentPostulationsController(
        IStudentPostulationCommandService commandService,
        IStudentPostulationQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentPostulationRequest request)
    {
        var postulation = await _commandService.CreateAsync(request.StudentId, request.ProjectId);
        return Ok(StudentPostulationMapper.ToDto(postulation));
    }

    [HttpPut("{postulationId}/accept")]
    public async Task<IActionResult> Accept(int postulationId)
    {
        var postulation = await _commandService.AcceptAsync(postulationId);
        return Ok(StudentPostulationMapper.ToDto(postulation));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? studentId, [FromQuery] int? projectId)
    {
        if (studentId is not null)
        {
            var list = await _queryService.GetByStudentIdAsync(studentId.Value);
            return Ok(list.Select(StudentPostulationMapper.ToDto));
        }

        if (projectId is not null)
        {
            var list = await _queryService.GetByProjectIdAsync(projectId.Value);
            return Ok(list.Select(StudentPostulationMapper.ToDto));
        }

        return BadRequest("Debe proporcionar studentId o projectId.");
    }
}