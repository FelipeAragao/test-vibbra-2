using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public abstract class BaseRepository<T>(NotificationSystemDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly NotificationSystemDbContext Db = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public virtual async Task<bool> AddAsync(T entity)
    {
        DbSet.Add(entity);
        return await SaveChanges() > 0;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<T> GetAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        return await SaveChanges() > 0;
    }

    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose() => Db?.Dispose();
}