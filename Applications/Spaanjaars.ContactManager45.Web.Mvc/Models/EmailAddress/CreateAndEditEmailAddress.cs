using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class CreateAndEditEmailAddress : IValidatableObject
  {
    public int Id { get; set; }
    public int PersonId { get; set; }
    [Required]
    [EmailAddress]
    public string EmailAddressText { get; set; }
    public ContactType ContactType { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (ContactType == ContactType.None)
      {
        yield return new ValidationResult("ContactType can't be None.", new[] { "ContactType" });
      }
    }
  }
}