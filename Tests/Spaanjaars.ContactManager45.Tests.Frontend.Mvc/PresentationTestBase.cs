using System.Diagnostics.CodeAnalysis;
using Spaanjaars.ContactManager45.Web.Mvc.App_Start;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Mvc
{
  [ExcludeFromCodeCoverage]
  public class PresentationTestBase
  {
    /// <summary>
    /// Initializes a new instance of the PresentationTestBase class.
    /// </summary>
    public PresentationTestBase()
    {
      AutoMapperConfig.Start();
    }
  }
}
