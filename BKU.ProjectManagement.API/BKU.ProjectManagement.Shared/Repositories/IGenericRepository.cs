using BKU.ProjectManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Shared.Repositories
{
    public interface IGenericRepository<T, TContext> where T : class where TContext : DbContext
    {
        DbContext dbContext { get; set; }
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> GetById(string id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task Delete(string id);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IQueryable<T> GetByConditionQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool tracking = false);
        Task<PagedResult<T>> GetWithPaging(int page, int pageSize, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task Insert(List<T> entities);
        Task Update(List<T> entities);
        Task Delete(Expression<Func<T, bool>> filter = null);
        Task<DateTime?> GetMaxDateTime(Expression<Func<T, bool>> filter = null, Expression<Func<T, DateTime?>> selector = null);
    }
}
