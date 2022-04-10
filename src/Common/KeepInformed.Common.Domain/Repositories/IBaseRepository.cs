using KeepInformed.Common.Domain.Entities;

namespace KeepInformed.Common.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> Get(Guid id);
    IQueryable<TEntity> GetAll();
    Task Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task SaveChanges();
}
