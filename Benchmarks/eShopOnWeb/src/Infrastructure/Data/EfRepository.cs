using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CatalogContext _dbContext;

        public EfRepository(CatalogContext dbContext) // @issue@I02
        {
            _dbContext = dbContext; // @issue@I02
        }

        public virtual T GetById(int id) // @issue@I02
        {
            return _dbContext.Set<T>().Find(id); // @issue@I02
        }

        public T GetSingleBySpec(ISpecification<T> spec) // @issue@I02
        {
            return List(spec).FirstOrDefault(); // @issue@I02
        }


        public virtual async Task<T> GetByIdAsync(int id) // @issue@I02
        {
            return await _dbContext.Set<T>().FindAsync(id); // @issue@I02
        }

        public IEnumerable<T> ListAll() // @issue@I02
        {
            return _dbContext.Set<T>().AsEnumerable(); // @issue@I02
        }

        public async Task<List<T>> ListAllAsync() // @issue@I02
        {
            return await _dbContext.Set<T>().ToListAsync(); // @issue@I02
        }

        public IEnumerable<T> List(ISpecification<T> spec) // @issue@I02
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes // @issue@I02
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings // @issue@I02
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult // @issue@I02
                            .Where(spec.Criteria)
                            .AsEnumerable();
        }
        public async Task<List<T>> ListAsync(ISpecification<T> spec) // @issue@I02
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes // @issue@I02
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings // @issue@I02
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult // @issue@I02
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        public T Add(T entity) // @issue@I02
        {
            _dbContext.Set<T>().Add(entity); // @issue@I02
            _dbContext.SaveChanges(); // @issue@I02

            return entity; // @issue@I02
        }

        public async Task<T> AddAsync(T entity) // @issue@I02
        {
            _dbContext.Set<T>().Add(entity); // @issue@I02
            await _dbContext.SaveChangesAsync(); // @issue@I02

            return entity; // @issue@I02
        }

        public void Update(T entity) // @issue@I02
        {
            _dbContext.Entry(entity).State = EntityState.Modified; // @issue@I02
            _dbContext.SaveChanges(); // @issue@I02
        }
        public async Task UpdateAsync(T entity) // @issue@I02
        {
            _dbContext.Entry(entity).State = EntityState.Modified; // @issue@I02
            await _dbContext.SaveChangesAsync(); // @issue@I02
        }

        public void Delete(T entity) // @issue@I02
        {
            _dbContext.Set<T>().Remove(entity); // @issue@I02
            _dbContext.SaveChanges(); // @issue@I02
        }
        public async Task DeleteAsync(T entity) // @issue@I02
        {
            _dbContext.Set<T>().Remove(entity); // @issue@I02
            await _dbContext.SaveChangesAsync(); // @issue@I02
        }
    }
}
