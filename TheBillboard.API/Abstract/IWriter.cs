﻿using Dapper;

namespace TheBillboard.API.Abstract
{
    public interface IWriter
    {
        Task<bool> WriteAsync(string query, DynamicParameters parameters);
    }
}