using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Repositories.EF
{
  /// <summary>
  /// Serves as a generic base class for concrete repositories to support basic CRUD oprations on data in the system.
  /// </summary>
  /// <typeparam name="T">The type of entity this repository works with. Must be a class inheriting DomainEntity.</typeparam>
  public abstract class Repository<T> : IRepository<T, int>, IDisposable where T : DomainEntity<int>
  {
    /// <summary>
    /// Finds an item by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the item in the database.</param>
    /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
    /// <returns>The requested item when found, or null otherwise.</returns>
    public virtual T FindById(int id, params Expression<Func<T, object>>[] includeProperties)
    {
      return FindAll(includeProperties).SingleOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Returns an IQueryable of all items of type T.
    /// </summary>
    /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
    /// <returns>An IQueryable of the requested type T.</returns>
    public virtual IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
    {
      IQueryable<T> items = DataContextFactory.GetDataContext().Set<T>();

      if (includeProperties != null)
      {
        foreach (var includeProperty in includeProperties)
        {
          items = items.Include(includeProperty);
        }
      }
      return items;
    }

    /// <summary>
    /// Returns an IQueryable of items of type T.
    /// </summary>
    /// <param name="predicate">A predicate to limit the items being returned.</param>
    /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
    /// <returns>An IEnumerable of the requested type T.</returns>
    public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
      IQueryable<T> items = DataContextFactory.GetDataContext().Set<T>();
      if (includeProperties != null)
      {
        foreach (var includeProperty in includeProperties)
        {
          items = items.Include(includeProperty);
        }
      }
      return items.Where(predicate);
    }

    /// <summary>
    /// Adds an entity to the underlying DbContext.
    /// </summary>
    /// <param name="entity">The entity that should be added.</param>
    public virtual void Add(T entity)
    {
      DataContextFactory.GetDataContext().Set<T>().Add(entity);
    }

    /// <summary>
    /// Removes an entity from the underlying DbContext.
    /// </summary>
    /// <param name="entity">The entity that should be removed.</param>
    public virtual void Remove(T entity)
    {
      DataContextFactory.GetDataContext().Set<T>().Remove(entity);
    }

    /// <summary>
    /// Removes an entity from the underlying DbContext. Calls <see cref="FindById" /> to resolve the item.
    /// </summary>
    /// <param name="id">The ID of the entity that should be removed.</param>
    public virtual void Remove(int id)
    {
      Remove(FindById(id));
    }

    /// <summary>
    /// Disposes the underlying data context.
    /// </summary>
    public void Dispose()
    {
      if (DataContextFactory.GetDataContext() != null)
      {
        DataContextFactory.GetDataContext().Dispose();
      }
    }
  }
}
