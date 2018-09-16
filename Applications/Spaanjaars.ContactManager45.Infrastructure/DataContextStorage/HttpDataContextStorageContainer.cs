using System.Web;

namespace Spaanjaars.Infrastructure.DataContextStorage
{
  /// <summary>
  /// A Helper class to store objects like a DataContext in the HttpContext.Current.Items collection.
  /// </summary>
  /// <typeparam name="T">The type of object to store.</typeparam>
  public class HttpDataContextStorageContainer<T> : IDataContextStorageContainer<T> where T : class
  {
    private const string DataContextKey = "DataContext";

    /// <summary>
    /// Returns an object from the container when it exists. Returns null otherwise.
    /// </summary>
    /// <returns>The object from the container when it exists, null otherwise.</returns>
    public T GetDataContext()
    {
      T objectContext = null;
      if (HttpContext.Current.Items.Contains(DataContextKey))
      {
        objectContext = (T)HttpContext.Current.Items[DataContextKey];
      }
      return objectContext;
    }

    /// <summary>
    /// Clears the object from the container.
    /// </summary>
    public void Clear()
    {
      if (HttpContext.Current.Items.Contains(DataContextKey))
      {
        HttpContext.Current.Items[DataContextKey] = null;
      }
    }

    /// <summary>
    /// Stores the object in HttpContext.Current.Items.
    /// </summary>
    /// <param name="objectContext">The object to store.</param>
    public void Store(T objectContext)
    {
      if (HttpContext.Current.Items.Contains(DataContextKey))
      {
        HttpContext.Current.Items[DataContextKey] = objectContext;
      }
      else
      {
        HttpContext.Current.Items.Add(DataContextKey, objectContext);
      }
    }
  }
}
