using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Spaanjaars.ContactManager45.Model;
using System.Web.UI;
using Spaanjaars.ContactManager45.Web.WebForms.Helpers;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.WebForms.People
{
  public partial class PhoneNumbers : Page
  {
    public IQueryable<PhoneNumber> ListPhoneNumbers([QueryString("Id")] int personId)
    {
      var repo = RepositoryHelpers.GetPeopleRepository();
      var person = repo.FindById(personId, x => x.PhoneNumbers);
      return person.PhoneNumbers.AsQueryable();
    }

    public void InsertPhoneNumber([QueryString("Id")] int personId, PhoneNumber phoneNumber)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
          {
            var repo = RepositoryHelpers.GetPeopleRepository();
            var person = repo.FindById(personId, x => x.PhoneNumbers);
            var userNumber = new PhoneNumber { OwnerId = personId };
            TryUpdateModel(userNumber);
            person.PhoneNumbers.Add(userNumber);
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

    public void UpdatePhoneNumber([QueryString("Id")] int personId, int id, PhoneNumber phoneNumber)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
          {
            var repo = RepositoryHelpers.GetPeopleRepository();
            var person = repo.FindById(personId, x => x.PhoneNumbers);
            PhoneNumber userNumber = person.PhoneNumbers.Single(x => x.Id == id);
            TryUpdateModel(userNumber);
            PhoneNumbersGrid.EditIndex = -1;
            PhoneNumbersGrid.DataBind();
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

    public void DeletePhoneNumber([QueryString("Id")] int personId, int id)
    {
      using (RepositoryHelpers.GetUnitOfWorkFactory().Create())
      {
        var repo = RepositoryHelpers.GetPeopleRepository();
        var person = repo.FindById(personId, x => x.PhoneNumbers);
        person.PhoneNumbers.Remove(person.PhoneNumbers.Single(x => x.Id == id));
      }
    }

    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
      PhoneNumbersGrid.DataBind();
    }

    public IEnumerable<ListItem> GetContactTypes()
    {
      return ListItemHelpers.EnumToListItems<ContactType>();
    }

    protected void PhoneNumbersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      var typeDropDown = (DropDownList) PhoneNumbersGrid.Rows[e.RowIndex].FindControl("ContactType");
      e.NewValues["ContactType"] = (ContactType) Convert.ToInt32(typeDropDown.SelectedValue);
    }
  }
}