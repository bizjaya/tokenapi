using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TOKENAPI.EF;

namespace TOKENAPI.Common
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<List<T>> GetAll();

        bool Any(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

        T Single(Expression<Func<T, bool>> predicate);
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        Task AddAsync(T entity);

        void Update(T entity);
        Task UpdateAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);


        void Remove(T entity);
        void Remove(long id);
        void RemoveRange(IEnumerable<T> entities);

        void Delete(object Id);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbCtx Context;
        private DbSet<T> _entities;

        public const int defPageNum = 1;
        public const int defPageSize = 50;

        public Repository(DbCtx context)
        {
            Context = context;
            _entities = Context.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> Get(long id)
        {
            return await _entities.FindAsync(id);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _entities.Any(predicate);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.AnyAsync(predicate);
        }

        public Task<List<T>> GetAll()
        {
            return _entities.ToListAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)   /*smell*/
        {
            return await _entities.Where(predicate).ToListAsync();
        }
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindIncAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var que = _entities.Where(predicate);
            foreach (var inc in includes)
            {
                que = que.Include(inc);
            }
            return await que.ToListAsync();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public async void Remove(long Id)
        {
            var item = await _entities.FindAsync(Id);
            if (item != null)
            {
                _entities.Remove(item);
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }


        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _entities.SingleUpdateAsync(entity);
        }

        public async Task UpdateRangeAsync(List<T> entity)
        {
            await Task.Run(() => _entities.UpdateRange(entity));
        }

        public async void Delete(object Id)
        {
            T entity = await _entities.FindAsync(Id);
            _entities.Remove(entity);
        }


    }
}
