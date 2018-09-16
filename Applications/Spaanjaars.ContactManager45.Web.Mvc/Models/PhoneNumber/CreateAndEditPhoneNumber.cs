using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class CreateAndEditPhoneNumber : IValidatableObject
  {
    public int Id { get; set; }
    [Required]
    public string Number { get; set; }
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