using System.Web;

namespace Spaanjaars.Infrastructure.DataContextStorage
{
  /// <summary>
  /// A Helper class to store objects like a DataContext in the HttpContext.Current.Items collection.
  /// </summary>
  /// <typeparam name="T">The type of object to store.</typeparam>
  public class HttpDataContextStorageContainer<T> : IDataContextStorageContainer<T> where T : class
  {
    private string dataContextKey;
    
    public HttpDataContextStorageContainer()
    {
      dataContextKey = "context" + typeof(T);
    }

    /// <summary>
    /// Returns an object from the container when it exists. Returns null otherwise.
    /// </summary>
    /// <returns>The object from the container when it exists, null otherwise.</returns>
    public T GetDataContext()
    {
      T objectContext = null;
      if (HttpContext.Current.Items.Contains(dataContextKey))
      {
        objectContext = (T)HttpContext.Current.Items[dataContextKey];
      }
      return objectContext;
    }

    /// <summary>
    /// Clears the object from the container.
    /// </summary>
    public void Clear()
    {
      if (HttpContext.Current.Items.Contains(dataContextKey))
      {
        HttpContext.Current.Items[dataContextKey] = null;
      }
    }

    /// <summary>
    /// Stores the object in HttpContext.Current.Items.
    /// </summary>
    /// <param name="objectContext">The object to store.</param>
    public void Store(T objectContext)
    {
      if (HttpContext.Current.Items.Contains(dataContextKey))
      {
        HttpContext.Current.Items[dataContextKey] = objectContext;
      }
      else
      {
        HttpContext.Current.Items.Add(dataContextKey, objectContext);
      }
    }
  }
}
