using UniTalents_BackEnd_AW.Shared.Domain.Repositories;
using UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Configurations;

namespace UniTalents_BackEnd_AW.Shared.Infrastructure.Persistence.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}
