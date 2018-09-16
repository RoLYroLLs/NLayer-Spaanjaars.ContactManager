using System.Diagnostics.CodeAnalysis;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Mvc
{
  [ExcludeFromCodeCoverage]
  public class FakeUnitOfWorkFactory : IUnitOfWorkFactory
  {
    public IUnitOfWork Create()
    {
      throw new System.NotImplementedException();
    }

    public IUnitOfWork Create(bool forceNew)
    {
      throw new System.NotImplementedException();
    }
  }
}
