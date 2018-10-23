using System.Diagnostics.CodeAnalysis;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [ExcludeFromCodeCoverage]
  public class FakeUnitOfWork<T> : IUnitOfWork<T>
  {

    public void Commit(bool resetAfterCommit)
    {
     
    }

    public void Undo()
    {
     
    }

    public void Dispose()
    {
      
    }
  }
}