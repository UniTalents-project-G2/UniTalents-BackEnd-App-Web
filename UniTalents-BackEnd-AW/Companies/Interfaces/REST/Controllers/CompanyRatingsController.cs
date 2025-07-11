using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;          // ← NUEVO
using UniTalents_BackEnd_AW.Companies.Interfaces.REST.Resources;
using UniTalents_BackEnd_AW.Students.Domain.Repositories;

namespace UniTalents_BackEnd_AW.Companies.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CompanyRatingsController : ControllerBase
{
    private readonly ICompanyRatingCommandService _command;
    private readonly ICompanyRatingRepository     _ratingRepo;   // ← NUEVO
    private readonly IStudentRepository           _studentRepo;

    public CompanyRatingsController(
        ICompanyRatingCommandService command,
        ICompanyRatingRepository     ratingRepo,   // ← NUEVO
        IStudentRepository           studentRepo)
    {
        _command     = command;
        _ratingRepo  = ratingRepo;   // ← NUEVO
        _studentRepo = studentRepo;
    }

    // POST /api/companyratings
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRatingRequest body)
    {
        // 1. userId del token
        var userIdClaim =
            User.FindFirst("userId") ??
            User.FindFirst(ClaimTypes.NameIdentifier) ??
            User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized("Token sin userId.");
        if (!int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized("userId inválido.");

        // 2. Obtener studentId
        var student = await _studentRepo.GetByUserIdAsync(userId);
        if (student is null) return Unauthorized("No eres un estudiante registrado.");

        // 3. Crear rating
        var ratingRow = await _command.CreateAsync(body.ProjectId, body.Rating, student.Id);

        return Ok(new
        {
            ratingRow.Id,
            ratingRow.StudentId,
            ratingRow.ProjectId,
            ratingRow.Rating
        });
    }

    // GET /api/companyratings/exists?projectId=123
    [HttpGet("exists")]
    public async Task<IActionResult> HasRated([FromQuery] int projectId)
    {
        var userIdClaim =
            User.FindFirst("userId") ??
            User.FindFirst(ClaimTypes.NameIdentifier) ??
            User.FindFirst("sub");
        if (userIdClaim is null) return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized();

        var student = await _studentRepo.GetByUserIdAsync(userId);
        if (student is null) return Ok(new { rated = false });

        bool rated = await _ratingRepo.ExistsAsync(student.Id, projectId);
        return Ok(new { rated });
    }
}
