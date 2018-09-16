using Spaanjaars.Infrastructure.DataContextStorage;

namespace Spaanjaars.ContactManager45.Repositories.EF
{
  /// <summary>
  /// Manages instances of the ContactManagerContext and stores them in an appropriate storage container.
  /// </summary>
  public static class DataContextFactory
  {
    /// <summary>
    /// Clears out the current ContactManagerContext.
    /// </summary>
    public static void Clear()
    {
      var dataContextStorageContainer = DataContextStorageFactory<ContactManagerContext>.CreateStorageContainer();
      dataContextStorageContainer.Clear();
    }

    /// <summary>
    /// Retrieves an instance of ContactManagerContext from the appropriate storage container or
    /// creates a new instance and stores that in a container.
    /// </summary>
    /// <returns>An instance of ContactManagerContext.</returns>
    public static ContactManagerContext GetDataContext()
    {
      var dataContextStorageContainer = DataContextStorageFactory<ContactManagerContext>.CreateStorageContainer();
      var contactManagerContext = dataContextStorageContainer.GetDataContext();
      if (contactManagerContext == null)
      {
        contactManagerContext = new ContactManagerContext();
        dataContextStorageContainer.Store(contactManagerContext);
      }
      return contactManagerContext;
    }
  }
}
