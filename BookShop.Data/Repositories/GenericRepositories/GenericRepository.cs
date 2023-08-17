using System.Linq.Expressions;
using BookShop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Repositories.GenericRepositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{

    private readonly AppDbContext _dbContext;
    protected DbSet<TEntity> DbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        this.DbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>>? expression = null)
        => expression is null ? this.DbSet : this.DbSet.Where(expression);

    public async Task<TEntity?> SelectSingleAsync(Expression<Func<TEntity, bool>> expression)
        => await this.DbSet.SingleOrDefaultAsync(expression);

    public async Task<TEntity?> SelectFirstAsync(Expression<Func<TEntity, bool>> expression)
        => await this.DbSet.FirstOrDefaultAsync(expression);

    public async Task<bool> CheckConditionAsync(Expression<Func<TEntity, bool>> expression)
        => await this.DbSet.AnyAsync(expression);

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}