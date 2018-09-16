using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class CreateAndEditPerson : IValidatableObject
  {
    public int Id { get; set; }

    [Required, DisplayName("First name")]
    public string FirstName { get; set; }

    [Required, DisplayName("Last name")]
    public string LastName { get; set; }

    [DisplayName("Date of birth")]
    public DateTime DateOfBirth { get; set; }

    public PersonType Type { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Type == PersonType.None)
      {
        yield return new ValidationResult("PersonType can't be None.", new[] { "Type" });
      }
    }
  }
}
