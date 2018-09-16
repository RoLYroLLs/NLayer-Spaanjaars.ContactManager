using System.Data.Entity.ModelConfiguration;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Repositories.EF.Configuration
{
  /// <summary>
  /// Configures the behavior for a phone number in the model and the database.
  /// </summary>
  public class PhoneNumberConfiguration : EntityTypeConfiguration<PhoneNumber>
  {
    /// <summary>
    /// Initializes a new instance of the PhoneNumberConfiguration class.
    /// </summary>
    public PhoneNumberConfiguration()
    {
      Property(x => x.Number).HasMaxLength(25);
    }
  }
}
