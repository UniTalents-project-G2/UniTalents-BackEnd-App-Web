using Microsoft.AspNetCore.Mvc;
using UniTalents_BackEnd_AW.Reputations.Application.Internal.Services;
using UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Transform;

namespace UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReputationsController : ControllerBase
{
    private readonly IReputationCommandService _command;
    private readonly IReputationQueryService   _query;

    public ReputationsController(
        IReputationCommandService command,
        IReputationQueryService   query)
    {
        _command = command;
        _query   = query;
    }

    // POST /api/reputations
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReputationRequest body)
    {
        var rep = await _command.CreateAsync(body.ProjectId, body.Rating, body.Comment);
        return Ok(ReputationMapper.ToDto(rep));
    }

    // GET /api/reputations?studentId=1  ó  ?projectId=10
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? studentId, [FromQuery] int? projectId)
    {
        if (studentId is not null)
        {
            var list = await _query.GetByStudentIdAsync(studentId.Value);
            return Ok(list.Select(ReputationMapper.ToDto));
        }

        if (projectId is not null)
        {
            var list = await _query.GetByProjectIdAsync(projectId.Value);
            return Ok(list.Select(ReputationMapper.ToDto));
        }

        return BadRequest("Debe indicar studentId o projectId.");
    }
}