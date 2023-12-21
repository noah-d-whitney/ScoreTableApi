using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ScoreTableApi.IRepository;

public interface IBaseRepository<T> where T : class
{
    Task<ICollection<T>> GetAll();
    Task<T> Get(int id);
    Task<EntityEntry<T>> Create(T entity);
    Task<bool> Exists(int id);
    Task<EntityEntry<T>> Delete(int id);
}