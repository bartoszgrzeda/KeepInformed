﻿using KeepInformed.Application.Tvn.Repositories;
using KeepInformed.Domain.Tvn.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Repositories.Tvn;

public class TvnNewsRepository : BaseRepository<TvnNews>, ITvnNewsRepository
{
    public TvnNewsRepository(KeepInformedContext context) : base(context)
    {
    }

    public async Task<TvnNews?> GetByTvnGuid(string tvnGuid)
    {
        return await Entities.SingleOrDefaultAsync(x => x.TvnGuid == tvnGuid);
    }
}
