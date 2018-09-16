using System;
using FileHelpers;

namespace Spaanjaars.ContactManager.Import
{
  /// <summary>
  /// Used as a source type for the FileHelpers library.
  /// </summary>
  [DelimitedRecord(",")]
  public class ImportPerson
  {
    public string FirstName;
    public string LastName;
    [FieldConverter(ConverterKind.Date, "M/d/yyyy")]
    public DateTime? DateOfBirth;
    public int Type;
    public string Address;
    public string Zip;
    public string City;
    public string Country;
    public string Address2;
    public string Zip2;
    public string City2;
    public string Country2;
    public string Email;
    public string EmailType;
    public string PhoneNumber;
  }
}

