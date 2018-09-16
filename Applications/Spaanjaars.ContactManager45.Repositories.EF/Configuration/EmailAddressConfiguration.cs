using System.Data.Entity.ModelConfiguration;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Repositories.EF.Configuration
{
  /// <summary>
  /// Configures the behavior for an e-mail address in the model and the database.
  /// </summary>
  public class EmailAddressConfiguration : EntityTypeConfiguration<EmailAddress>
  {
    /// <summary>
    /// Initializes a new instance of the EmailAddressConfiguration class.
    /// </summary>
    public EmailAddressConfiguration()
    {
      Property(x => x.EmailAddressText).HasMaxLength(250);
    }
  }
}
