using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class EmailAddresses : Page
  {
    public IQueryable<EmailAddress> ListEmailAddresses([QueryString("Id")] int personId)
    {
      var repo = RepositoryHelpers.GetPeopleRepository();
      var person = repo.FindById(personId, x => x.EmailAddresses);
      return person.EmailAddresses.AsQueryable();
    }

    public void InsertEmailAddress([QueryString("Id")] int personId, EmailAddress emailAddress)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
          {
            var repo = RepositoryHelpers.GetPeopleRepository();
            var person = repo.FindById(personId, x => x.EmailAddresses);
            var userAddress = new EmailAddress { OwnerId = personId };
            TryUpdateModel(userAddress);
            person.EmailAddresses.Add(userAddress);
          }
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

    public void UpdateEmailAddress([QueryString("Id")] int personId, int id, EmailAddress emailAddress)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
          {
            var repo = RepositoryHelpers.GetPeopleRepository();
            var person = repo.FindById(personId, x => x.EmailAddresses);
            EmailAddress userAddress = person.EmailAddresses.Single(x => x.Id == id);
            TryUpdateModel(userAddress);
            EmailAddressesGrid.EditIndex = -1;
            EmailAddressesGrid.DataBind();
          }
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

    public void DeleteEmailAddress([QueryString("Id")] int personId, int id)
    {
      using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
      {
        var repo = RepositoryHelpers.GetPeopleRepository();
        var person = repo.FindById(personId, x => x.EmailAddresses);
        person.EmailAddresses.Remove(person.EmailAddresses.Single(x => x.Id == id));
      }
    }

    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
      EmailAddressesGrid.DataBind();
    }

    public IEnumerable<ListItem> GetContactTypes()
    {
      return ListItemHelpers.EnumToListItems<ContactType>();
    }

protected void EmailAddressesGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
{
  var typeDropDown = (DropDownList)EmailAddressesGrid.Rows[e.RowIndex].FindControl("ContactType");
  e.NewValues["ContactType"] = (ContactType)Convert.ToInt32(typeDropDown.SelectedValue);
}
  }
}