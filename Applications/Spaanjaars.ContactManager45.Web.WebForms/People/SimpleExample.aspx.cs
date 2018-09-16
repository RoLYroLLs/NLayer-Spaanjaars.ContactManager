using System.Collections.Generic;
using System.Web.UI;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class SimpleExample : Page
  {
    public IEnumerable<Person> FindAll()
    {
      return RepositoryHelpers.GetPeopleRepository().FindAll();
    }

    public void InsertPerson()
    {
      var person = new Person();
      TryUpdateModel(person);
      using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
      {
        RepositoryHelpers.GetPeopleRepository().Add(person);
      }
    }

    public void UpdatePerson(int id)
    {
      using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
      {
        var person = RepositoryHelpers.GetPeopleRepository().FindById(id);
        TryUpdateModel(person);
      }
    }

    public void DeletePerson(int id)
    {
      using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
      {
        RepositoryHelpers.GetPeopleRepository().Remove(id);
      }
    }
  }
}