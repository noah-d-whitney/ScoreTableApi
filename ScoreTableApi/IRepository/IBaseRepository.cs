using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ScoreTableApi.IRepository;

public interface IBaseRepository<T> where T : class
{
    Task<ICollection<T>> UserGetAll();
    Task<T> UserGet(int id);
    Task<EntityEntry<T>> Create(T entity);
    Task<bool> UserExists(int id);
    Task<EntityEntry<T>> UserDelete(int id);
}