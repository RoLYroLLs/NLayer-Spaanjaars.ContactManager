using System;
using System.Web.UI;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class Details : Page
  {
    private int _personId;
    protected void Page_Load(object sender, EventArgs e)
    {
      string personIdAsString = Request.QueryString.Get("Id");
      if (string.IsNullOrEmpty(personIdAsString) || !int.TryParse(personIdAsString, out _personId))
      {
        Response.Redirect("~/");
      }
      LoadPerson();
    }

    private void LoadPerson()
    {
      var peopleRepository = RepositoryHelpers.GetPeopleRepository();
      var person = peopleRepository.FindById(_personId);
      if (person != null)
      {
        FirstName.Text = person.FirstName;
        LastName.Text = person.LastName;
        DateOfBirth.Text = person.DateOfBirth.ToString("d");
        Type.Text = person.Type.ToString();
      }
    }
  }
}