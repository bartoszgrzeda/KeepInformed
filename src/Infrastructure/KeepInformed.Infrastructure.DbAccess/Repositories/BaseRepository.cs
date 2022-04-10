using KeepInformed.Common.Domain.Entities;
using KeepInformed.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> Entities;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        Entities = dbContext.Set<TEntity>();
    }

    public async Task Add(TEntity entity)
    {
        await Entities.AddAsync(entity);
    }

    public async Task<TEntity?> Get(Guid id)
    {
        return await Entities.SingleOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Entities.AsQueryable();
    }

    public void Remove(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        Entities.Update(entity);
    }
}
