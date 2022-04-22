using KeepInformed.Application.News.Repositories;
using KeepInformed.Contracts.News.Common;
using KeepInformed.Domain.News.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Tvn.Mocks;

public class TvnNewsRepositoryMock : INewsRepository
{
    private readonly List<News> _storage;

    public TvnNewsRepositoryMock()
    {
        _storage = new List<News>();
    }

    public async Task Add(News entity)
    {
        _storage.Add(entity);
    }

    public async Task<News?> Get(Guid id)
    {
        return _storage.SingleOrDefault(x => x.Id == id);
    }

    public IQueryable<News> GetAll()
    {
        return _storage.AsQueryable();
    }

    public async Task<News?> GetBySourceAndCustomStringId(NewsSource source, string customStringId)
    {
        return _storage.SingleOrDefault(x => x.Source == source && x.CustomStringId == customStringId);
    }

    public void Remove(News entity)
    {
        _storage.Remove(entity);
    }

    public async Task SaveChanges()
    {
    }

    public void Update(News entity)
    {
        var toRemove = _storage.SingleOrDefault(x => x.Id == entity.Id);

        if (toRemove != null)
        {
            _storage.Remove(entity);
        }

        _storage.Add(entity);
    }
}
