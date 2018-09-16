using System.Web;

namespace Spaanjaars.Infrastructure.DataContextStorage
{
  /// <summary>
  /// A helper class to create application platform specific storage containers.
  /// </summary>
  /// <typeparam name="T">The type for which to create the container.</typeparam>
  public static class DataContextStorageFactory<T> where T : class
  {
    private static IDataContextStorageContainer<T> _dataContextStorageContainer;

    /// <summary>
    /// Creates a new container that uses HttpContext.Current.Items (when HttpContext.Current is not null) or Thread.Items.
    /// </summary>
    /// <returns>A contact storage container to store objects. </returns>
    public static IDataContextStorageContainer<T> CreateStorageContainer()
    {
      if (_dataContextStorageContainer == null)
      {
        if (HttpContext.Current == null)
        {
          _dataContextStorageContainer = new ThreadDataContextStorageContainer<T>();
        }
        else
        {
          _dataContextStorageContainer = new HttpDataContextStorageContainer<T>();
        }
      }
      return _dataContextStorageContainer;
    }
  }
}
