﻿using System.Linq;
using System.Web.UI;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Repositories.EF;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class Default1 : Page
  {
    public IQueryable<Person> ListPeople()
    {
      var repo = RepositoryHelpers<ContactManagerContext>.GetPeopleRepository();
      return repo.FindAll().OrderBy(x => x.Id);
    }

    public void DeletePerson(int id)
    {
      using (RepositoryHelpers<ContactManagerContext>.GetUnitOfWorkFactory().Create())
      {
        RepositoryHelpers<ContactManagerContext>.GetPeopleRepository().Remove(id);
      }
    }
  }
}