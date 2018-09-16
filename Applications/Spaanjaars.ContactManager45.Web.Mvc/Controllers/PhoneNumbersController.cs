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
  public class PhoneNumbersController : Controller
  {
    private readonly IPeopleRepository _peopleRepository;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Initializes a new instance of the PhoneNumbersController class.
    /// </summary>
    public PhoneNumbersController(IPeopleRepository peopleRepository, IUnitOfWorkFactory unitOfWorkFactory)
    {
      _peopleRepository = peopleRepository;
      _unitOfWorkFactory = unitOfWorkFactory;
    }

    public ActionResult List(int personId)
    {
      ViewBag.PersonId = personId;
      var person = _peopleRepository.FindById(personId, x => x.PhoneNumbers);
      var data = new List<DisplayPhoneNumber>();
      Mapper.Map(person.PhoneNumbers, data);
      return View(data);
    }

    public ActionResult Details(int id, int personId)
    {
      ViewBag.PersonId = personId;

      var person = _peopleRepository.FindById(personId, x => x.PhoneNumbers);
      var data = new DisplayPhoneNumber();
      Mapper.Map(person.PhoneNumbers.Single(x => x.Id == id), data);
      return View(data);
    }

    public ActionResult Create(int personId)
    {
      ViewBag.PersonId = personId;
      return View();
    }

    [HttpPost]
    public ActionResult Create(CreateAndEditPhoneNumber createAndEditPhoneNumber, int personId)
    {
      ViewBag.PersonId = personId;
      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            var person = _peopleRepository.FindById(personId);
            var phoneNumber = new PhoneNumber();
            Mapper.Map(createAndEditPhoneNumber, phoneNumber);
            person.PhoneNumbers.Add(phoneNumber);
            return RedirectToAction("List", new { personId });
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

      var person = _peopleRepository.FindById(personId, x => x.PhoneNumbers);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new CreateAndEditPhoneNumber();
      Mapper.Map(person.PhoneNumbers.Single(x => x.Id == id), data);
      return View(data);
    }

    [HttpPost]
    public ActionResult Edit(CreateAndEditPhoneNumber createAndEditPhoneNumber, int personId)
    {
      ViewBag.PersonId = personId;
      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            var person = _peopleRepository.FindById(personId, x => x.PhoneNumbers);
            var phoneNUmber = person.PhoneNumbers.Single(x => x.Id == createAndEditPhoneNumber.Id);
            Mapper.Map(createAndEditPhoneNumber, phoneNUmber);
            return RedirectToAction("List", new { personId });
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
      ViewBag.PersonId = personId;

      var person = _peopleRepository.FindById(personId, x => x.PhoneNumbers);
      var data = new DisplayPhoneNumber();
      Mapper.Map(person.PhoneNumbers.Single(x => x.Id == id), data);
      return View(data);
    }

    [HttpPost]
    public ActionResult Delete(DisplayPhoneNumber displayPhoneNumber, int personId)
    {
      ViewBag.PersonId = personId;

      using (_unitOfWorkFactory.Create())
      {
        var person = _peopleRepository.FindById(personId, x => x.PhoneNumbers);
        var phoneNumber = person.PhoneNumbers.Single(x => x.Id == displayPhoneNumber.Id);
        person.PhoneNumbers.Remove(phoneNumber);
        return RedirectToAction("List", new { personId });
      }
    }


  }
}
