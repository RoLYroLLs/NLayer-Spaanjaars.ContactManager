using System.Data.Entity;
using Spaanjaars.ContactManager45.Model.Repositories;
using Spaanjaars.ContactManager45.Repositories.EF;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.WebForms.Helpers
{
public static class RepositoryHelpers<T> where T: DbContext, new()
{
  public static IPeopleRepository GetPeopleRepository()
  {
    return new PeopleRepository();
  }

  public static IUnitOfWorkFactory<T> GetUnitOfWorkFactory() 
  {
    return new EFUnitOfWorkFactory<T>();
  }
}
}