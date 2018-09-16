using System.Diagnostics.CodeAnalysis;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [ExcludeFromCodeCoverage]
  public class FakeUnitOfWork : IUnitOfWork
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