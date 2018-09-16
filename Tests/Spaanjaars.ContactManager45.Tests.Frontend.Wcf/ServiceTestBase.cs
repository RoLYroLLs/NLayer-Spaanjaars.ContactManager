using System.Diagnostics.CodeAnalysis;
using Spaanjaars.ContactManager45.Web.Wcf;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [ExcludeFromCodeCoverage]
  public class ServiceTestBase
  {
    /// <summary>
    /// Initializes a new instance of the ServiceTestBase class.
    /// </summary>
    internal ServiceTestBase()
    {
      AutoMapperConfig.Start();
    }
  }
}
