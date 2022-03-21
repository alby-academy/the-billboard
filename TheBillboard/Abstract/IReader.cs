﻿using Npgsql;
using System.Data;
using TheBillboard.Models;

namespace TheBillboard.Abstract;

public interface IReader
{
    public Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector, IEnumerable<(string, object)> parameters = default!);
}