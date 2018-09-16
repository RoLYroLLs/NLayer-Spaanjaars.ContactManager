using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;
using Spaanjaars.ContactManager45.Web.Mvc.Models;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.Mvc.Controllers
{
  public class EmailAddressesController : Controller
  {
    private readonly IPeopleRepository _peopleRepository;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Initializes a new instance of the EmailAddressesController class.
    /// </summary>
    public EmailAddressesController(IPeopleRepository peopleRepository, IUnitOfWorkFactory unitOfWorkFactory)
    {
      _peopleRepository = peopleRepository;
      _unitOfWorkFactory = unitOfWorkFactory;
    }

    public ActionResult List(int personId)
    {
      ViewBag.PersonId = personId;
      var person = _peopleRepository.FindById(personId, x => x.EmailAddresses);
      var data = new List<DisplayEmailAddress>();
      Mapper.Map(person.EmailAddresses, data);
      return View(data);
    }

    public ActionResult Details(int id, int personId)
    {
      var person = _peopleRepository.FindById(personId, x => x.EmailAddresses);
      var data = new DisplayEmailAddress();
      Mapper.Map(person.EmailAddresses.First(x => x.Id == id), data);
      return View(data);
    }

    public ActionResult Create(int personId)
    {
      ViewBag.PersonId = personId;
      return View();
    }

    [HttpPost]
    public ActionResult Create(CreateAndEditEmailAddress createAndEditEmailAddress)
    {
      ViewBag.PersonId = createAndEditEmailAddress.PersonId;
      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            var person = _peopleRepository.FindById(createAndEditEmailAddress.PersonId);
            var emailAddress = new EmailAddress();
            Mapper.Map(createAndEditEmailAddress, emailAddress);
            person.EmailAddresses.Add(emailAddress);
            return RedirectToAction("List", new { createAndEditEmailAddress.PersonId });
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

    public ActionResult Edit(int id, int personId)
    {
      ViewBag.PersonId = personId;

      var person = _peopleRepository.FindById(personId, x => x.EmailAddresses);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new CreateAndEditEmailAddress();
      Mapper.Map(person.EmailAddresses.Single(x => x.Id == id), data);
      return View(data);
    }

    [HttpPost]
    public ActionResult Edit(CreateAndEditEmailAddress createAndEditEmailAddress)
    {
      ViewBag.PersonId = createAndEditEmailAddress.PersonId;

      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            var person = _peopleRepository.FindById(createAndEditEmailAddress.PersonId, x => x.EmailAddresses);
            var emailAddress = person.EmailAddresses.Single(x => x.Id == createAndEditEmailAddress.Id);
            Mapper.Map(createAndEditEmailAddress, emailAddress);
            return RedirectToAction("List", new { createAndEditEmailAddress.PersonId });
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

    public ActionResult Delete(int id, int personId)
    {
      var person = _peopleRepository.FindById(personId, x => x.EmailAddresses);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new DisplayEmailAddress();
      Mapper.Map(person.EmailAddresses.Single(x => x.Id == id), data);
      return View(data);
    }

    [HttpPost]
    public ActionResult Delete(DisplayEmailAddress displayEmailAddress)
    {
      using (_unitOfWorkFactory.Create())
      {
        var person = _peopleRepository.FindById(displayEmailAddress.PersonId, x => x.EmailAddresses);
        var address = person.EmailAddresses.Single(x => x.Id == displayEmailAddress.Id);
        person.EmailAddresses.Remove(address);
        return RedirectToAction("List", new { displayEmailAddress.PersonId });
      }
    }
  }
}
