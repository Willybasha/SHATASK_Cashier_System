using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Interfaces;
using InfrastructureLayer.Context;
using Microsoft.EntityFrameworkCore;


namespace InfrastructureLayer.Repository.GenericRepo
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _context;
        public RepositoryBase(AppDbContext repositoryContext)
        => _context = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _context.Set<T>().AsNoTracking()
            : _context.Set<T>();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
            => !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() :
                                _context.Set<T>().Where(expression);
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void CreateRange(IEnumerable<T> entity) => _context.Set<T>().AddRange(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public void DeleteRange(IEnumerable<T> entity)=>_context.Set<T>().RemoveRange(entity);

        public void UpdateRange(IEnumerable<T> entity)=>_context.Set<T>().UpdateRange(entity);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)=>
            await _context.Set<T>().AnyAsync(predicate);
        
    }
}
