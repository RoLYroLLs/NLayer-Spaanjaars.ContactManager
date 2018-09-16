using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Web.Mvc.App_Start;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Mvc
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class AutoMapperTests : PresentationTestBase
  {
    [TestMethod]
    public void AllMappingIsValid()
    {
      AutoMapperConfig.Start(); // Should crash when configuration is invalid.
    }
  }
}
