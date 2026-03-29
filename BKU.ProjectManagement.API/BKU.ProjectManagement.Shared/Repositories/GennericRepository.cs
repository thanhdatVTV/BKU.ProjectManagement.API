using BKU.ProjectManagement.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Shared.Repositories
{
    public class GenericRepository<T, TContext> : IGenericRepository<T, TContext> where T : class where TContext : DbContext
    {
        public DbContext dbContext { get; set; }
        internal DbSet<T> dbSet;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GenericRepository(DbContext dbContext, IConfiguration config, IHttpContextAccessor httpContextAccessor = null)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Delete(Guid id)
        {
            T entityToDelete = dbSet.Find(id);
            await Delete(entityToDelete);
        }
        public async Task Delete(string id)
        {
            T entityToDelete = dbSet.Find(id);
            await Delete(entityToDelete);
        }
        public async Task Delete(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            dbSet.RemoveRange(query);
            await dbContext.SaveChangesAsync();
        }
        public async Task Delete(T entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            await dbContext.SaveChangesAsync();
        }
        public async Task Insert(T entity)
        {
            AuditData(entity, true);
            dbSet.Add(entity);
            await SaveChangesAsyncAndDetached();
        }
        public async Task Insert(List<T> entities)
        {
            AuditData(entities, true);
            dbContext.Set<T>().AddRange(entities);
            await SaveChangesAsyncAndDetached();
        }
        public async Task Update(T entity)
        {
            AuditData(entity, false);
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsyncAndDetached();
        }
        public async Task Update(List<T> entities)
        {
            AuditData(entities, false);
            foreach (T entity in entities)
            {
                dbContext.Set<T>().Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            await SaveChangesAsyncAndDetached();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return await GetByConditionQueryable(filter, orderBy, includeProperties).ToListAsync();
        }
        public async Task<T> GetById(Guid id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            if (entity != null)
                dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<T> GetById(string id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<Models.PagedResult<T>> GetWithPaging(int page, int pageSize, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = GetByConditionQueryable(filter, orderBy, includeProperties);
            var result = new Models.PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();
            return result;
        }
        public IQueryable<T> GetByConditionQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool tracking = false)
        {
            IQueryable<T> query = dbSet;

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }

        public async Task<DateTime?> GetMaxDateTime(Expression<Func<T, bool>> filter = null, Expression<Func<T, DateTime?>> selector = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }
            var maxDateTime = await query.MaxAsync(selector);
            return maxDateTime;
        }
        
        //========================= Private Function ===========================================
        private void AuditData(T entity, bool isCreated)
        {
            if (isCreated)
            {
                entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now);
                entity.GetType().GetProperty("CreatedBy")?.SetValue(entity, _httpContextAccessor?.HttpContext != null
                ? _httpContextAccessor.GetCurrentOperateUser()
                : null);
            }
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
            {
                entity.GetType().GetProperty("UpdatedBy")?.SetValue(entity, _httpContextAccessor.GetCurrentOperateUser());
            }
            entity.GetType().GetProperty("UpdatedDate").SetValue(entity, DateTime.Now);
        }
        private void AuditData(List<T> entities, bool isCreated)
        {
            foreach (T entity in entities)
            {
                AuditData(entity, isCreated);
            }
        }
        private async Task SaveChangesAsyncAndDetached()
        {
            await dbContext.SaveChangesAsync();
            foreach (var changeTracker in dbContext.ChangeTracker.Entries())
            {
                changeTracker.State = EntityState.Detached;
            }
        }
    }
}
