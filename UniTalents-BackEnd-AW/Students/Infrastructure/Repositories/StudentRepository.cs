using Microsoft.EntityFrameworkCore;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;
using UniTalents_BackEnd_AW.Students.Domain.Entities;
using UniTalents_BackEnd_AW.Students.Domain.Repositories;


namespace UniTalents_BackEnd_AW.Students.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task<Student?> GetByUserIdAsync(int userId)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }


    public void Update(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void Delete(Student student)
    {
        _context.Students.Remove(student);
    }
}