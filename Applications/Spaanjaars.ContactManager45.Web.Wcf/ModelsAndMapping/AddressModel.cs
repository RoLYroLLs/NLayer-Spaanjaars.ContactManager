using System.Runtime.Serialization;

namespace Spaanjaars.ContactManager45.Web.Wcf
{
  [DataContract]
  public class AddressModel
  {
    [DataMember]
    public string Street { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public string ZipCode { get; set; }
    [DataMember]
    public string Country { get; set; }
  }
}