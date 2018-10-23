using System.Diagnostics.CodeAnalysis;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [ExcludeFromCodeCoverage]
  public class FakeUnitOfWorkFactory<T> : IUnitOfWorkFactory<T>
  {
    public IUnitOfWork<T> Create()
    {
      return Create(false);
    }

    public IUnitOfWork<T> Create(bool forceNew)
    {
      return new FakeUnitOfWork<T>();
    }
  }
}
