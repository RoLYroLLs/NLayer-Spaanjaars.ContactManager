using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spaanjaars.ContactManager45.Web.Wcf
{
  [DataContract]
  public class ServiceResult<T>
  {
    /// <summary>
    /// Initializes a new instance of the ServiceResult class.
    /// </summary>
    public ServiceResult()
    {
      Errors = new List<ValidationResultModel>();
    }

    [DataMember]
    public T Data { get; internal set; }
    [DataMember]
    public IEnumerable<ValidationResultModel> Errors { get; internal set; }
  }
}