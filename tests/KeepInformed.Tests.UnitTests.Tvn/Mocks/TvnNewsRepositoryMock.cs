using KeepInformed.Application.Tvn.Repositories;
using KeepInformed.Domain.Tvn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Tvn.Mocks;

public class TvnNewsRepositoryMock : ITvnNewsRepository
{
    private readonly List<TvnNews> _storage;

    public TvnNewsRepositoryMock()
    {
        _storage = new List<TvnNews>();
    }

    public async Task Add(TvnNews entity)
    {
        _storage.Add(entity);
    }

    public async Task<TvnNews?> Get(Guid id)
    {
        return _storage.SingleOrDefault(x => x.Id == id);
    }

    public IQueryable<TvnNews> GetAll()
    {
        return _storage.AsQueryable();
    }

    public async Task<TvnNews?> GetByTvnGuid(string tvnGuid)
    {
        return _storage.SingleOrDefault(x => x.TvnGuid == tvnGuid);
    }

    public void Remove(TvnNews entity)
    {
        _storage.Remove(entity);
    }

    public async Task SaveChanges()
    {
    }

    public void Update(TvnNews entity)
    {
        var toRemove = _storage.SingleOrDefault(x => x.Id == entity.Id);

        if (toRemove != null)
        {
            _storage.Remove(entity);
        }

        _storage.Add(entity);
    }
}
