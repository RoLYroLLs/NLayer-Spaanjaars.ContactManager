using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class EditAddress : Page
  {
    private int _personId;
    private ContactType _contactType;

    protected void Page_Load(object sender, EventArgs e)
    {
      string personIdAsString = Request.QueryString.Get("Id");
      if (string.IsNullOrEmpty(personIdAsString) || !int.TryParse(personIdAsString, out _personId))
      {
        Response.Redirect("~/");
      }
      if (string.IsNullOrEmpty(Request.QueryString.Get("ContactType")))
      {
        Response.Redirect("~/");
      }
      _contactType = (ContactType)Enum.Parse(typeof(ContactType), Request.QueryString.Get("ContactType"));

      if (!Page.IsPostBack)
      {
        LoadAddress();
      }
    }

    private void LoadAddress()
    {
      var peopleRepository = RepositoryHelpers.GetPeopleRepository();
      var person = peopleRepository.FindById(_personId);
      if (person != null)
      {
        Address address = _contactType == ContactType.Personal ? person.HomeAddress : person.WorkAddress;
        Street.Text = address.Street;
        City.Text = address.City;
        ZipCode.Text = address.ZipCode;
        Country.Text = address.Country;
      }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
      var address = new Address(Street.Text, City.Text, ZipCode.Text, Country.Text, _contactType );
      var errors = address.Validate();
      foreach (var error in errors)
      {
        ModelState.AddModelError("", error.ErrorMessage);
      }
      if (ModelState.IsValid)
      {
        using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
        {
          var repository = RepositoryHelpers.GetPeopleRepository();
          Person person = repository.FindById(_personId);
          switch (_contactType)
          {
            case ContactType.Business:
              person.WorkAddress = address;
              break;
            case ContactType.Personal:
              person.HomeAddress = address;
              break;
            default:
              throw new Exception("Unsupported ContactType.");
          }
        }
        Response.Redirect("Default.aspx");
      }
    }
  }
}