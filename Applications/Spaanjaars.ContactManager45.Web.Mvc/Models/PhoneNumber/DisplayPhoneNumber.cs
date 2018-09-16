using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class DisplayPhoneNumber
  {
    public int Id { get; set; }
    public string Number { get; set; }
    public ContactType ContactType { get; set; }
  }
}