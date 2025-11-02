using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AmbulanceSystem.DAL;
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    protected readonly DbSet<TEntity> entities;

    public GenericRepository(AmbulanceDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        this.entities = context.Set<TEntity>();
    }

    public TEntity GetById(int id)
    {
        return entities.Find(id);
    }

    public void Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        entities.Add(entity);
    }

    public void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        entities.Update(entity);
    }

    public void Remove(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            entities.Remove(entity);
        }
    }

    public List<TEntity> GetAll()
    {
        return entities.ToList();
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return entities.FirstOrDefault(predicate);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return entities.AsQueryable(); // повертає запит, який ще не виконано
    }
}
