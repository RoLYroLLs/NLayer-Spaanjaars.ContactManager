using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using Spaanjaars.ContactManager45.Repositories.EF;

namespace Spaanjaars.ContactManager45.Tests.Integration
{
  [ExcludeFromCodeCoverage]
  public class IntegrationTestBase
  {
    internal IntegrationTestBase()
    {
      // Use LocalDB for Entity Framework by default
      Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

      ContactManagerContextInitializer.Init(true);
    }
  }
}
