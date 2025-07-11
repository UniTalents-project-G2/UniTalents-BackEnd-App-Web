namespace UniTalents_BackEnd_AW.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    
    Task CompleteAsync();
    
}