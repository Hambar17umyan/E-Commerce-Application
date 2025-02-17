﻿using API.Models.Control.ResultModels;
using FluentResults;

namespace API.Data.Repositories.Interfaces
{
    public interface IDataRepository<T>
    {
        public Task<InnerResult> AddAsync(T entity);
        public IEnumerable<T> GetAll();
        public InnerResult<T> GetBy(Func<T, bool> predicate);
        public Task<InnerResult> RemoveAsync(T entity);
        public Task<InnerResult> UpdateAsync(Func<T, bool> predicate, Action<T> action);
        public InnerResult<T> GetById(int id);
        public IEnumerable<T> GetAllWithQuery(Func<IQueryable<T>, IQueryable<T>> query);
    }
}
