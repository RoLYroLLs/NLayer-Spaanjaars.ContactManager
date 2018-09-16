using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Repositories.EF
{
  /// <summary>
  /// Defines a Unit of Work using an EF DbContext under the hood.
  /// </summary>
  public class EFUnitOfWork : IUnitOfWork
  {
    /// <summary>
    /// Initializes a new instance of the EFUnitOfWork class.
    /// </summary>
    /// <param name="forceNewContext">When true, clears out any existing data context first.</param>
    public EFUnitOfWork(bool forceNewContext)
    {
      if (forceNewContext)
      {
        DataContextFactory.Clear();
      }
    }

    /// <summary>
    /// Saves the changes to the underlying DbContext.
    /// </summary>
    public void Dispose()
    {
      DataContextFactory.GetDataContext().SaveChanges();
    }

    /// <summary>
    /// Saves the changes to the underlying DbContext.
    /// </summary>
    /// <param name="resetAfterCommit">When true, clears out the data context afterwards.</param>
  public void Commit(bool resetAfterCommit)
  {
    DataContextFactory.GetDataContext().SaveChanges();
    if (resetAfterCommit)
    {
      DataContextFactory.Clear();
    }
  }

  /// <summary>
  /// Undoes changes to the current DbContext by removing it from the storage container.
  /// </summary>
  public void Undo()
  {
    DataContextFactory.Clear();
  }
  }
}
