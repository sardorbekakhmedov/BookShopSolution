using System.Linq.Expressions;

namespace BookShop.Data.Repositories.GenericRepositories;

public interface IGenericRepository<TEntity> where TEntity: class
{
    public Task<TEntity> InsertAsync(TEntity entity);
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>>? expression = null);
    public Task<TEntity?> SelectSingleAsync(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity?> SelectFirstAsync(Expression<Func<TEntity, bool>> expression);
    public Task<bool> CheckConditionAsync(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task DeleteAsync(TEntity entity);
}