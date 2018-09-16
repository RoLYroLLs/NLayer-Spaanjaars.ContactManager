using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Model
{
  /// <summary>
  /// Represents an address of a person (visit or postal).
  /// </summary>
  public class Address : ValueObject<Address>
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the Address class.
    /// The constructor is marked private because we want other consuming code to use the overloaded constructor.
    /// However, EF still needs a parameterless constructor.
    /// </summary>
    private Address() { }

    /// <summary>
    /// Initializes a new instance of the Address class.
    /// </summary>
    /// <param name="street">The street of this address.</param>
    /// <param name="city">The city of this address.</param>
    /// <param name="zipCode">The zip code of this address.</param>
    /// <param name="country">The country of this address.</param>
    /// <param name="contactType">The type of this address.</param>
    public Address(string street, string city, string zipCode, string country, ContactType contactType)
    {
      Street = street;
      City = city;
      ZipCode = zipCode;
      Country = country;
      ContactType = contactType;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the street of this address.
    /// </summary>
    public string Street { get; private set; }

    /// <summary>
    /// Gets the zip code  of this address.
    /// </summary>
    public string ZipCode { get; private set; }

    /// <summary>
    /// Gets the city of this address.
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// Gets the country of this address.
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    /// Gets the contact type of this address. 
    /// </summary>
    public ContactType ContactType { get; private set; }

    /// <summary>
    /// Determines if this address can be considered to represent a "null" value.
    /// </summary>
    // <returns>True when all four properties of the address contain null; false otherwise. 
    public bool IsNull
    {
      get
      {
        return (string.IsNullOrEmpty(Street) && string.IsNullOrEmpty(ZipCode) && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Country));
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Validates this object. 
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (!IsNull)
      {
        if (ContactType == ContactType.None)
        {
          yield return new ValidationResult("ContactType can't be None.", new[] { "ContactType" });
        }
        if (string.IsNullOrEmpty(Street))
        {
          yield return new ValidationResult("Street can't be null or empty", new[] { "Street" });
        }
        if (string.IsNullOrEmpty(ZipCode))
        {
          yield return new ValidationResult("ZipCode can't be null or empty", new[] { "ZipCode" });
        }
        if (string.IsNullOrEmpty(City))
        {
          yield return new ValidationResult("City can't be null or empty", new[] { "City" });
        }
        if (string.IsNullOrEmpty(Country))
        {
          yield return new ValidationResult("Country can't be null or empty", new[] { "Country" });
        }
      }
    }
    #endregion
  }
}
