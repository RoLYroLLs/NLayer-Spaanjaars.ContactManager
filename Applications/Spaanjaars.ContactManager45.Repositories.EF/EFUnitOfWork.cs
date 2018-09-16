using System.Data.Entity;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Repositories.EF
{
  /// <summary>
  /// Defines a Unit of Work using an EF DbContext under the hood.
  /// </summary>
  public class EFUnitOfWork<T> : IUnitOfWork where T : DbContext
  {
    /// <summary>
    /// Initializes a new instance of the EFUnitOfWork class.
    /// </summary>
    /// <param name="forceNewContext">When true, clears out any existing data context first.</param>
    public EFUnitOfWork(bool forceNewContext)
    {
      if (forceNewContext)
      {
        DataContextFactory<T>.Clear();
      }
    }

    /// <summary>
    /// Saves the changes to the underlying DbContext.
    /// </summary>
    public void Dispose()
    {
      DataContextFactory<T>.GetDataContext().SaveChanges();
    }

    /// <summary>
    /// Saves the changes to the underlying DbContext.
    /// </summary>
    /// <param name="resetAfterCommit">When true, clears out the data context afterwards.</param>
  public void Commit(bool resetAfterCommit)
  {
    DataContextFactory<T>.GetDataContext().SaveChanges();
    if (resetAfterCommit)
    {
      DataContextFactory<T>.Clear();
    }
  }

  /// <summary>
  /// Undoes changes to the current DbContext by removing it from the storage container.
  /// </summary>
  public void Undo()
  {
    DataContextFactory<T>.Clear();
  }
  }
}
