using KeepInformed.Common.Domain.Entities;
using KeepInformed.Common.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Mocks;

public class BaseRespositoryMock<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly List<TEntity> _storage;

    public BaseRespositoryMock()
    {
        _storage = new List<TEntity>();
    }

    public async Task Add(TEntity entity)
    {
        _storage.Add(entity);
    }

    public async Task<TEntity?> Get(Guid id)
    {
        return _storage.SingleOrDefault(x => x.Id == id);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _storage.AsQueryable();
    }

    public void Remove(TEntity entity)
    {
        _storage.Remove(entity);
    }

    public async Task SaveChanges()
    {
    }

    public void Update(TEntity entity)
    {
        var toRemove = _storage.SingleOrDefault(x => x.Id == entity.Id);

        if (toRemove != null)
        {
            Remove(toRemove);
        }

        _storage.Add(entity);
    }
}
