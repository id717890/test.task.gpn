using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oil.Dal.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly OilDbContext Context;

        public BaseRepository(OilDbContext context)
        {
            Context = context;
        }

        public virtual void Add(T entity)
        {
            Context.Entry(entity);
            Context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            Context.Entry(entity);
            await Context.Set<T>().AddAsync(entity);
        }

        public virtual void AddOrUpdate(T entity, bool commitChanges)
        {
            if (entity.Id == 0)
            {
                Add(entity);
            }
            else
            {
                Edit(entity);
            }

            if (commitChanges)
            {
                Commit();
            }
        }

        public async Task AddOrUpdateAsync(T entity, bool commitChanges)
        {
            if (entity.Id == 0)
            {
                await AddAsync(entity);
            }
            else
            {
                Edit(entity);
            }

            if (commitChanges)
            {
                await CommitAsync();
            }
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public virtual void Commit()
        {
            Context.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
            Commit();
        }

        public void Edit(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).ToListAsync();
        }


        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetSingleAsync(Int64 id)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(Int64 id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
