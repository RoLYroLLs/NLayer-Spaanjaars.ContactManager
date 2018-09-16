using System.Diagnostics.CodeAnalysis;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [ExcludeFromCodeCoverage]
  public class FakeUnitOfWorkFactory : IUnitOfWorkFactory
  {
    public IUnitOfWork Create()
    {
      return Create(false);
    }

    public IUnitOfWork Create(bool forceNew)
    {
      return new FakeUnitOfWork();
    }
  }
}
