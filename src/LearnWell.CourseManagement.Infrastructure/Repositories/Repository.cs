
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LearnWell.CourseManagement.Infrastructure.Repositories;
internal abstract class Repository<TEntity, TEntityId> 
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext) => DbContext = dbContext;

    public async Task<TEntity> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public virtual void Add(TEntity entity) => DbContext.Add(entity);


    public virtual void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }
    public virtual void Delete(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);

    public virtual async Task DeleteByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is not null)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
    public virtual async Task PatchAsync(TEntityId id, Dictionary<string, object> updatedFields, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"{typeof(TEntity).Name} with id '{id}' not found.");

        var entry = DbContext.Entry(entity);

        foreach (var field in updatedFields)
        {
            entry.Property(field.Key).CurrentValue = field.Value;
            entry.Property(field.Key).IsModified = true;
        }
    }
}
