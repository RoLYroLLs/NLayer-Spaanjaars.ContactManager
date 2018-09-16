using System.Data.Entity.ModelConfiguration;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Repositories.EF.Configuration
{
  /// <summary>
  /// Configures the behavior for a person in the model and the database.
  /// </summary>
  public class PersonConfiguration : EntityTypeConfiguration<Person>
  {
    /// <summary>
    /// Initializes a new instance of the PersonConfiguration class.
    /// </summary>
    public PersonConfiguration()
    {
      Property(x => x.FirstName).IsRequired().HasMaxLength(25);
      Property(x => x.LastName).IsRequired().HasMaxLength(25);

      Property(x => x.HomeAddress.Street).HasColumnName("HomeAddressStreet").HasMaxLength(50);
      Property(x => x.HomeAddress.City).HasColumnName("HomeAddressCity").HasMaxLength(50);
      Property(x => x.HomeAddress.ZipCode).HasColumnName("HomeAddressZipCode").HasMaxLength(15);
      Property(x => x.HomeAddress.Country).HasColumnName("HomeAddressCountry").HasMaxLength(30);
      Property(x => x.HomeAddress.ContactType).HasColumnName("HomeAddressContactType");

      Property(x => x.WorkAddress.Street).HasColumnName("WorkAddressStreet").HasMaxLength(50);
      Property(x => x.WorkAddress.City).HasColumnName("WorkAddressCity").HasMaxLength(50);
      Property(x => x.WorkAddress.ZipCode).HasColumnName("WorkAddressZipCode").HasMaxLength(15);
      Property(x => x.WorkAddress.Country).HasColumnName("WorkAddressCountry").HasMaxLength(30);
      Property(x => x.WorkAddress.ContactType).HasColumnName("WorkAddressContactType");
    }
  }
}
