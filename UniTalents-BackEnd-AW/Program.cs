using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Security;

// Users
using UniTalents_BackEnd_AW.Users.Application.Internal;
using UniTalents_BackEnd_AW.Users.Application.Internal.Services;
using UniTalents_BackEnd_AW.Users.Domain.Repositories;
using UniTalents_BackEnd_AW.Users.Infrastructure.Repositories;

// Students
using UniTalents_BackEnd_AW.Students.Application.Internal.Services;
using UniTalents_BackEnd_AW.Students.Domain.Repositories;
using UniTalents_BackEnd_AW.Students.Infrastructure.Internal.Services;
using UniTalents_BackEnd_AW.Students.Infrastructure.Repositories;

// Companies
using UniTalents_BackEnd_AW.Companies.Application.Internal.Services;
using UniTalents_BackEnd_AW.Companies.Domain.Repositories;
using UniTalents_BackEnd_AW.Companies.Infrastructure.Internal.Services;
using UniTalents_BackEnd_AW.Companies.Infrastructure.Repositories;

// Projects
using UniTalents_BackEnd_AW.Projects.Application.Internal.Services;
using UniTalents_BackEnd_AW.Projects.Domain.Repositories;
using UniTalents_BackEnd_AW.Projects.Infrastructure.Internal.Services;
using UniTalents_BackEnd_AW.Projects.Infrastructure.Repositories;

// StudentPostulations
using UniTalents_BackEnd_AW.StudentPostulations.Application.Internal.Services;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Repositories;
using UniTalents_BackEnd_AW.StudentPostulations.Infrastructure.Internal.Services;
using UniTalents_BackEnd_AW.StudentPostulations.Infrastructure.Repositories;

// Reputations
using UniTalents_BackEnd_AW.Reputations.Application.Internal.Services;
using UniTalents_BackEnd_AW.Reputations.Domain.Repositories;
using UniTalents_BackEnd_AW.Reputations.Infrastructure.Internal.Services;
using UniTalents_BackEnd_AW.Reputations.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// 🔗 DbContext (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

// 🌐 CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173", 
                "https://purple-beach-0c0218c10.2.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey   = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = jwtSettings["Issuer"],
        ValidAudience            = jwtSettings["Audience"],
        IssuerSigningKey         = new SymmetricSecurityKey(secretKey)
    };
});

#region 🔧 Dependency Injection

// Users
builder.Services.AddScoped<IUserRepository,                UserRepository>();
builder.Services.AddScoped<IUserCommandService,            UserCommandService>();
builder.Services.AddScoped<IUserQueryService,              UserQueryService>();

// Students
builder.Services.AddScoped<IStudentRepository,             StudentRepository>();
builder.Services.AddScoped<IStudentCommandService,         StudentCommandService>();
builder.Services.AddScoped<IStudentQueryService,           StudentQueryService>();

// Companies
builder.Services.AddScoped<ICompanyRepository,             CompanyRepository>();
builder.Services.AddScoped<ICompanyCommandService,         CompanyCommandService>();
builder.Services.AddScoped<ICompanyQueryService,           CompanyQueryService>();

// Projects
builder.Services.AddScoped<IProjectRepository,             ProjectRepository>();
builder.Services.AddScoped<IProjectCommandService,         ProjectCommandService>();
builder.Services.AddScoped<IProjectQueryService,           ProjectQueryService>();

// Student Postulations
builder.Services.AddScoped<IStudentPostulationRepository,  StudentPostulationRepository>();
builder.Services.AddScoped<IStudentPostulationCommandService, StudentPostulationCommandService>();
builder.Services.AddScoped<IStudentPostulationQueryService,   StudentPostulationQueryService>();

// Reputations
builder.Services.AddScoped<IReputationRepository,          ReputationRepository>();
builder.Services.AddScoped<IReputationCommandService,      ReputationCommandService>();
builder.Services.AddScoped<IReputationQueryService,        ReputationQueryService>();

// Company Ratings  👈 NUEVO
builder.Services.AddScoped<ICompanyRatingRepository,       CompanyRatingRepository>();
builder.Services.AddScoped<ICompanyRatingCommandService,   CompanyRatingCommandService>();

// Token generator
builder.Services.AddScoped<ITokenService, TokenService>();

#endregion

// 📦 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🎮 Controllers
builder.Services.AddControllers();

var app = builder.Build();

// 🌟 Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();          // CORS antes de auth
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// 🚀 Ensure DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
