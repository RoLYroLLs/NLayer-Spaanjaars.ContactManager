using System;
using System.Runtime.Serialization;

namespace Spaanjaars.ContactManager45.Web.Wcf
{
  [DataContract]
  public class PersonModel
  {
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string FirstName { get; set; }
    [DataMember]
    public string LastName { get; set; }
    [DataMember]
    public DateTime DateOfBirth { get; set; }
    [DataMember]
    public AddressModel HomeAddress { get; set; }
    [DataMember]
    public AddressModel WorkAddress { get; set; }
  }
}
