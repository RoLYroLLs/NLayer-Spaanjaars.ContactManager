using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;
using Spaanjaars.ContactManager45.Web.Mvc.Models;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.Mvc.Controllers
{
  public class AddressesController : Controller
  {
    private readonly IPeopleRepository _peopleRepository;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Initializes a new instance of the AddressesController class.
    /// </summary>
    public AddressesController(IPeopleRepository peopleRepository, IUnitOfWorkFactory unitOfWorkFactory)
    {
      _peopleRepository = peopleRepository;
      _unitOfWorkFactory = unitOfWorkFactory;
    }

    public ActionResult Edit(int personId, ContactType contactType)
    {
      var person = _peopleRepository.FindById(personId);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new EditAddress();
      Mapper.Map(contactType == ContactType.Personal ? person.HomeAddress : person.WorkAddress, data);
      return View(data);
    }

    [HttpPost]
    public ActionResult Edit(EditAddress editAddressModel)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            var person = _peopleRepository.FindById(editAddressModel.PersonId);
            Mapper.Map(editAddressModel, editAddressModel.ContactType == ContactType.Personal ? person.HomeAddress : person.WorkAddress);
            return RedirectToAction("Index", "People");
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
      return View();
    }
  }
}
