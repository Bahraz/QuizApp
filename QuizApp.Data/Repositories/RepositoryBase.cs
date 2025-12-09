using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Interfaces;
using System.Linq.Expressions;

namespace QuizApp.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly QuizDbContext _db;

        protected RepositoryBase(QuizDbContext db)
        {
            _db = db;
        }

        public virtual async Task<T> GetByIdAsync(int id)
            => await _db.Set<T>().FindAsync(id);

        public virtual IQueryable<T> GetAll()
            => _db.Set<T>();

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _db.Set<T>().Where(predicate).ToListAsync();

        public virtual async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _db.Set<T>().AddRangeAsync(entities);
        }

        public virtual void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
        }

        public Task SaveChangesAsync()
            => _db.SaveChangesAsync();
    }
}
