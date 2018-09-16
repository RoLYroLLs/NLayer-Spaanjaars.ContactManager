using System;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class DisplayPerson
  {
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public PersonType Type { get; set; }
  }
}