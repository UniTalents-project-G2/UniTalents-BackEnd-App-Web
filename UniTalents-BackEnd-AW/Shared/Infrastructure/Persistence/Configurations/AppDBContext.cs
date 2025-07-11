using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Companies.Domain.Entities;
using UniTalents_BackEnd_AW.Projects.Domain.Entities;
using UniTalents_BackEnd_AW.Reputations.Domain.Entities;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations.Extensions;
using UniTalents_BackEnd_AW.Students.Domain.Entities;
using UniTalents_BackEnd_AW.Users.Domain.Entities;
using UniTalents_BackEnd_AW.StudentPostulations.Domain.Entities;

namespace UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User>               Users               { get; set; }
        public DbSet<Student>            Students            { get; set; }
        public DbSet<Company>            Companies           { get; set; }
        public DbSet<Project>            Projects            { get; set; }
        public DbSet<StudentPostulation> StudentPostulations { get; set; }
        public DbSet<Reputation>         Reputations         { get; set; }
        public DbSet<CompanyRating>      CompanyRatings      { get; set; }   // 👈 nuevo DbSet

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseSnakeCaseNamingConvention();

            // Users -------------------------------------------------
            modelBuilder.Entity<User>().ToTable("Users");

            // Students ---------------------------------------------
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>()
                .Property(s => s.SpecializationsJson).HasColumnName("specializations_json");
            modelBuilder.Entity<Student>()
                .Property(s => s.EndedProjectsJson).HasColumnName("ended_projects_json");

            // Companies --------------------------------------------
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Company>().Property(c => c.Specializations)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.None).ToList()
                );

            // Projects ---------------------------------------------
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().Property(p => p.Skills)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            modelBuilder.Entity<Project>().Property(p => p.Postulants)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Length == 0
                        ? new List<int>()
                        : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                );
            modelBuilder.Entity<Project>().Property(p => p.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<Project>().Property(p => p.UpdatedAt).HasColumnName("updated_at");

            // StudentPostulations ----------------------------------
            modelBuilder.Entity<StudentPostulation>().ToTable("StudentPostulations");
            modelBuilder.Entity<StudentPostulation>().HasKey(p => p.Id);
            modelBuilder.Entity<StudentPostulation>().Property(p => p.StudentId).HasColumnName("student_id").IsRequired();
            modelBuilder.Entity<StudentPostulation>().Property(p => p.ProjectId).HasColumnName("project_id").IsRequired();
            modelBuilder.Entity<StudentPostulation>().Property(p => p.Status).HasColumnName("status").IsRequired();
            modelBuilder.Entity<StudentPostulation>().Property(p => p.Date).HasColumnName("date").IsRequired();
            modelBuilder.Entity<StudentPostulation>().Property(p => p.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<StudentPostulation>().Property(p => p.UpdatedAt).HasColumnName("updated_at");

            // Reputations ------------------------------------------
            modelBuilder.Entity<Reputation>().ToTable("Reputations");
            modelBuilder.Entity<Reputation>().HasKey(r => r.Id);
            modelBuilder.Entity<Reputation>().Property(r => r.StudentId).HasColumnName("student_id").IsRequired();
            modelBuilder.Entity<Reputation>().Property(r => r.ProjectId).HasColumnName("project_id").IsRequired();
            modelBuilder.Entity<Reputation>().Property(r => r.Rating).HasColumnName("rating").IsRequired();
            modelBuilder.Entity<Reputation>().Property(r => r.Comment).HasColumnName("comment").IsRequired();
            modelBuilder.Entity<Reputation>().Property(r => r.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<Reputation>().Property(r => r.UpdatedAt).HasColumnName("updated_at");

            // CompanyRatings ---------------------------------------
            modelBuilder.Entity<CompanyRating>().ToTable("company_ratings");
            modelBuilder.Entity<CompanyRating>().HasKey(cr => cr.Id);
            modelBuilder.Entity<CompanyRating>().Property(cr => cr.StudentId).HasColumnName("student_id").IsRequired();
            modelBuilder.Entity<CompanyRating>().Property(cr => cr.ProjectId).HasColumnName("project_id").IsRequired();
            modelBuilder.Entity<CompanyRating>().Property(cr => cr.Rating).HasColumnName("rating").IsRequired();
            modelBuilder.Entity<CompanyRating>().Property(cr => cr.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<CompanyRating>().Property(cr => cr.UpdatedAt).HasColumnName("updated_at");
            modelBuilder.Entity<CompanyRating>()
                         .HasIndex(cr => new { cr.StudentId, cr.ProjectId })
                         .IsUnique(); // evita calificación duplicada
        }
    }
}
