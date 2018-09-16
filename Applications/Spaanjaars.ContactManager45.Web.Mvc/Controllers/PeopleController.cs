using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using AutoMapper;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;
using Spaanjaars.ContactManager45.Web.Mvc.Models;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.Mvc.Controllers
{
  public class PeopleController : BaseController
  {
    private readonly IPeopleRepository _peopleRepository;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    const int PageSize = 10;

    /// <summary>
    /// Initializes a new instance of the PeopleController class.
    /// </summary>
    public PeopleController(IPeopleRepository peopleRepository, IUnitOfWorkFactory unitOfWorkFactory)
    {
      _peopleRepository = peopleRepository;
      _unitOfWorkFactory = unitOfWorkFactory;
    }

    public ActionResult Index(int page = 1, string sort = "Id", string sortDir = "ASC")
    {
      int totalRecords = _peopleRepository.FindAll().Count();
      var data = new List<DisplayPerson>();
      IQueryable<Person> allPeople = _peopleRepository.FindAll().OrderBy(BuildOrderBy(sort, sortDir)).Skip((page * PageSize) - PageSize).Take(PageSize);
      Mapper.Map(allPeople, data);
      var model = new PagerModel<DisplayPerson> { Data = data, PageNumber = page, PageSize = PageSize, TotalRows = totalRecords };
      return View(model);
    }

    private string BuildOrderBy(string sortOn, string sortDirection)
    {
      if (sortOn.ToLower() == "fullname")
      {
        return String.Format("FirstName {0}, LastName {0}", sortDirection);
      }
      return string.Format("{0} {1}", sortOn, sortDirection);
    }

    public ActionResult Details(int id)
    {
      Person person = _peopleRepository.FindById(id);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new DisplayPerson();
      Mapper.Map(person, data);
      return View(data);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateAndEditPerson createAndEditPerson)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            Person person = new Person();
            Mapper.Map(createAndEditPerson, person);
            _peopleRepository.Add(person);
            return RedirectToAction("Index");
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

    public ActionResult Edit(int id)
    {
      Person person = _peopleRepository.FindById(id);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new CreateAndEditPerson();
      Mapper.Map(person, data);
      return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(CreateAndEditPerson createAndEditPerson)
    {
      if (ModelState.IsValid)
      {
        try
        {
          using (_unitOfWorkFactory.Create())
          {
            Person personToUpdate = _peopleRepository.FindById(createAndEditPerson.Id);
            Mapper.Map(createAndEditPerson, personToUpdate, typeof(CreateAndEditPerson), typeof(Person));
            return RedirectToAction("Index");
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

    public ActionResult Delete(int id)
    {
      Person person = _peopleRepository.FindById(id);
      if (person == null)
      {
        return HttpNotFound();
      }
      var data = new DisplayPerson();
      Mapper.Map(person, data);
      return View(data);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      using (_unitOfWorkFactory.Create())
      {
        _peopleRepository.Remove(id);
      }
      return RedirectToAction("Index");
    }
  }
}