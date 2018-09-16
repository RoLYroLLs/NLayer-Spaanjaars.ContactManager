using Spaanjaars.ContactManager45.Model.Repositories;
using Spaanjaars.ContactManager45.Repositories.EF;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.WebForms.Helpers
{
public static class RepositoryHelpers
{
  public static IPeopleRepository GetPeopleRepository()
  {
    return new PeopleRepository();
  }

  public static IUnitOfWorkFactory GetUnitOfWorkFactory()
  {
    return new EFUnitOfWorkFactory();
  }
}
}