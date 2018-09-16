using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class DisplayEmailAddress
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string EmailAddressText { get; set; }
    public ContactType ContactType { get; set; }
  }
}