namespace UniTalents_BackEnd_AW.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> FindByIdAsync(int id);
    Task<IEnumerable<TEntity>> ListAsync();
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}