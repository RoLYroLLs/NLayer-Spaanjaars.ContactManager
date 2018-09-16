using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Model
{
  /// <summary>
  /// Represents an e-mail address in the system.
  /// </summary>
  public class EmailAddress : DomainEntity<int>, IHasOwner
  {
    #region Properties

    /// <summary>
    /// Gets or sets the text of the e-mail address.
    /// </summary>
    [Required]
    [EmailAddressAttribute]
    public string EmailAddressText { get; set; }

    /// <summary>
    /// Gets or sets the type of the e-mail address.
    /// </summary>
    public ContactType ContactType { get; set; }

    /// <summary>
    /// Gets or sets the owner (a Person) of the e-mail address.
    /// </summary>
    public Person Owner { get; set; }

    /// <summary>
    /// Gets or sets the ID of the owner (a Person) of the e-mail address.
    /// </summary>
    public int OwnerId { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Determines whether this object is valid or not.
    /// </summary>
    /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
    /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (ContactType == ContactType.None)
      {
        yield return new ValidationResult("ContactType can't be None.", new[] { "ContactType" });
      }
    }
    #endregion
  }
}
