using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.ContactManager45.Model.Collections;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Model
{
  /// <summary>
  /// Represents a contact person in the system.
  /// </summary>
  public class Person : DomainEntity<int>, IDateTracking
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the Person class.
    /// </summary>
    public Person()
    {
      EmailAddresses = new EmailAddresses();
      PhoneNumbers = new PhoneNumbers();
      HomeAddress = new Address(null, null, null, null, ContactType.Personal);
      WorkAddress = new Address(null, null, null, null, ContactType.Business);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the date and time the object was created.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the date and time the object was last modified.
    /// </summary>
    public DateTime DateModified { get; set; }

    /// <summary>
    /// Gets or sets the first name of this person.
    /// </summary>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of this person.
    /// </summary>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of this person.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the type of person.
    /// </summary>
    public PersonType Type { get; set; }

    /// <summary>
    /// Gets or sets the home address of this person.
    /// </summary>
    public Address HomeAddress { get; set; }

    /// <summary>
    /// Gets or sets the work address of this person.
    /// </summary>
    public Address WorkAddress { get; set; }

    /// <summary>
    /// Gets or sets the e-mail addresses of this person.
    /// </summary>
    public EmailAddresses EmailAddresses { get; private set; }

    /// <summary>
    /// Gets or sets the phone numbers of this person.
    /// </summary>
    public PhoneNumbers PhoneNumbers { get; set; }

    /// <summary>
    /// Gets the full name of this person.
    /// </summary>
    public string FullName
    {
      get
      {
        string temp = FirstName ?? string.Empty;
        if (!string.IsNullOrEmpty(LastName))
        {
          if (temp.Length > 0)
          {
            temp += " ";
          }
          temp += LastName;
        }
        return temp;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Type == PersonType.None)
      {
        yield return new ValidationResult("Type can't be None.", new[] { "Type" });
      }

      if (DateOfBirth < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
      {
        yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
      }
      if (DateOfBirth > DateTime.Now)
      {
        yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
      }

      foreach (var result in PhoneNumbers.Validate())
      {
        yield return result;
      }

      foreach (var result in EmailAddresses.Validate())
      {
        yield return result;
      }

      foreach (var result in HomeAddress.Validate())
      {
        yield return result;
      }

      foreach (var result in WorkAddress.Validate())
      {
        yield return result;
      }
    }
    #endregion
  }
}
