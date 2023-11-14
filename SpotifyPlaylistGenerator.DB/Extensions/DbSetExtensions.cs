using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SpotifyPlaylistGenerator.DB.Extensions;

public static class DbSetExtensions
{
    public static async Task AddOrUpdateAsync<T>(
        this DbSet<T> dbSet, 
        T entity, 
        Expression<Func<T, bool>> identifierExpression) where T : class
    {
        var existingEntity = await dbSet.SingleOrDefaultAsync(identifierExpression);
    
        if (existingEntity != null)
        {
            dbSet.Update(entity);
        }
        else 
        {
            await dbSet.AddAsync(entity);
        }
    }

    public static async Task AddOrUpdateRangeAsync<T>(
        this DbSet<T> dbSet,
        IEnumerable<T> entities,
        Func<T, Expression<Func<T, bool>>> identifierExpression) where T : class
    {
        foreach (var entity in entities)
        {
            await dbSet.AddOrUpdateAsync(entity, identifierExpression(entity));
        }
    }
    
    
    public static async Task AddIfNotExistsAsync<T>(
        this DbSet<T> dbSet, 
        T entity, 
        Expression<Func<T, bool>> identifierExpression) where T : class
    {
        var existingEntity = await dbSet.SingleOrDefaultAsync(identifierExpression);
    
        if (existingEntity == null)
        {
            await dbSet.AddAsync(entity);
        }
    }

    public static async Task AddIfNotExistsRangeAsync<T>(
        this DbSet<T> dbSet,
        IEnumerable<T> entities,
        Func<T, Expression<Func<T, bool>>> identifierExpression) where T : class
    {
        foreach (var entity in entities)
        {
            await dbSet.AddIfNotExistsAsync(entity, identifierExpression(entity));
        }
    }
}