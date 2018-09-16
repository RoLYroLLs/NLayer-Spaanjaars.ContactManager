using Spaanjaars.ContactManager45.Repositories.EF;
using Spaanjaars.Infrastructure;
using StructureMap;

namespace Spaanjaars.ContactManager45.Web.Wcf.StructureMap
{
  public static class Ioc
  {
    public static void Initialize()
    {
      ObjectFactory.Initialize(scanner =>
      {
        scanner.Scan(scan =>
        {
          scan.AssembliesFromApplicationBaseDirectory();
          scan.WithDefaultConventions();
        });
        scanner.For<IUnitOfWorkFactory>().Use<EFUnitOfWorkFactory>();
      });
    }
  }
}
