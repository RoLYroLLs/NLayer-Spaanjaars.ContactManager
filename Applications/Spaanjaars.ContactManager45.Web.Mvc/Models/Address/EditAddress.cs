using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class EditAddress : IValidatableObject
  {
    public int PersonId { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public ContactType ContactType { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      return new Address(Street, City, ZipCode, Country, ContactType).Validate();
    }
  }
}