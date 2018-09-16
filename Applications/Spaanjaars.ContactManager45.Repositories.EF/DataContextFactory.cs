using System.Data.Entity;
using Spaanjaars.Infrastructure.DataContextStorage;

namespace Spaanjaars.ContactManager45.Repositories.EF
{
  /// <summary>
  /// Manages instances of the ContactManagerContext and stores them in an appropriate storage container.
  /// </summary>
  public static class DataContextFactory<T> where T : DbContext, new()
  {
    /// <summary>
    /// Clears out the current ContactManagerContext.
    /// </summary>
    public static void Clear()
    {
      var dataContextStorageContainer = DataContextStorageFactory<T>.CreateStorageContainer();
      dataContextStorageContainer.Clear();
    }

    /// <summary>
    /// Retrieves an instance of ContactManagerContext from the appropriate storage container or
    /// creates a new instance and stores that in a container.
    /// </summary>
    /// <returns>An instance of ContactManagerContext.</returns>
    public static T GetDataContext()
    {
      var dataContextStorageContainer = DataContextStorageFactory<T>.CreateStorageContainer();
      var contactManagerContext = dataContextStorageContainer.GetDataContext();
      if (contactManagerContext == null)
      {
        contactManagerContext = new T();
        dataContextStorageContainer.Store(contactManagerContext);
      }
      return contactManagerContext;
    }
  }
}
