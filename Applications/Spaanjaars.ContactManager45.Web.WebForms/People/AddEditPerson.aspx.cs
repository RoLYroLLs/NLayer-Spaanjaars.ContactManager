using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class AddEditPerson : Page
  {
    private int _personId;
    protected void Page_Load(object sender, EventArgs e)
    {
      string personIdAsString = Request.QueryString.Get("Id");
      if (!string.IsNullOrEmpty(personIdAsString) && !int.TryParse(personIdAsString, out _personId))
      {
        Response.Redirect("~/");
      }
      if (_personId == 0)
      {
        Title = "Add new contact person";
      }
      else
      {
        Title = "Edit contact person";
        if (!Page.IsPostBack)
        {
          LoadPerson();
        }
      }
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
        Type.DataBind();
        var item = Type.Items.FindByValue(((int)person.Type).ToString());
        if (item != null)
        {
          item.Selected = true;
        }
      }
    }

    public IEnumerable<ListItem> GetTypes()
    {
      return ListItemHelpers.EnumToListItems<PersonType>();
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
      try
      {
        using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
        {
          var repository = RepositoryHelpers.GetPeopleRepository();
          Person person;
          if (_personId == 0)
          {
            person = new Person();
            repository.Add(person);
          }
          else
          {
            person = repository.FindById(_personId);
          }

          person.FirstName = FirstName.Text;
          person.LastName = LastName.Text;
          person.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
          person.Type = (PersonType)Enum.Parse(typeof(PersonType), Type.SelectedValue);
        }
        Response.Redirect("Default.aspx");
      }
      catch (ModelValidationException mvex)
      {
        foreach (var error in mvex.ValidationErrors)
        {
          ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
        }
      }
    }
  }
}