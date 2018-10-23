using System.Data.Entity;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Repositories.EF
{

  /// <summary>
  /// Creates new instances of an EF unit of Work.
  /// </summary>
  public class EFUnitOfWorkFactory<T> : IUnitOfWorkFactory<T> where T: DbContext, new()
  {
    /// <summary>
    /// Creates a new instance of an EFUnitOfWork.
    /// </summary>
    public IUnitOfWork<T> Create()
    {
      return Create(false);
    }

    /// <summary>
    /// Creates a new instance of an EFUnitOfWork.
    /// </summary>
    /// <param name="forceNew">When true, clears out any existing data context from the storage container.</param>
    public IUnitOfWork<T> Create(bool forceNew)
    {
      return new EFUnitOfWork<T>(forceNew);
    }
  }
}
