using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Interfaces.Repository;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    // Create
    Task<bool> AddAsync(T entity);
    // Read
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    // Update
    Task<bool> UpdateAsync(T entity);
    Task<int> SaveChanges();
}