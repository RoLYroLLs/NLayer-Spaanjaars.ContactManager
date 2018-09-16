using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using NLog;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Web.Wcf
{
public class ContactManagerService : IContactManagerService
{
  private readonly IPeopleRepository _peopleRepository;
  private readonly IUnitOfWorkFactory _unitOfWorkFactory;

  private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

  public ContactManagerService(IPeopleRepository peopleRepository, IUnitOfWorkFactory unitOfWorkFactory)
  {
    if (peopleRepository == null)
    {
      Logger.Error("peopleRepository is null.");
      throw new ArgumentNullException("peopleRepository", "peopleRepository is null.");
    }
    if (unitOfWorkFactory == null)
    {
      Logger.Error("unitOfWorkFactory is null.");
      throw new ArgumentNullException("unitOfWorkFactory", "unitOfWorkFactory is null.");
    }
    _peopleRepository = peopleRepository;
    _unitOfWorkFactory = unitOfWorkFactory;
  }

  public PersonModel GetPerson(int id)
  {
    Logger.Trace("Getting person: {0}", id);
    var person = _peopleRepository.FindById(id);
    Logger.Trace("Person with ID: {0} is {1}null.", id, person == null ? "" : "not ");
    return Mapper.Map(person, new PersonModel());
  }

    public ServiceResult<int?> InsertPerson(PersonModel personModel)
    {
      var person = new Person();
      Mapper.Map(personModel, person);

      // For demo purposes, let's assume we only accept colleagues. You could easily add Type to the PersonModel and let external code set it
      person.Type = PersonType.Colleague;

      List<ValidationResult> errors = person.Validate().ToList();
   
      if (errors.Any())
      {
        var result = new ServiceResult<int?> { Errors = Mapper.Map(errors, new List<ValidationResultModel>()) };
        return result;
      }
      using (_unitOfWorkFactory.Create())
      {
        _peopleRepository.Add(person);
      }
      return new ServiceResult<int?> { Data = person.Id };
    }

    public ServiceResult<int?> UpdatePerson(PersonModel personModel)
    {
      var person = _peopleRepository.FindById(personModel.Id);
      if (person == null)
      {
        return new ServiceResult<int?> { Errors = new List<ValidationResultModel> { new ValidationResultModel { ErrorMessage = "Unknown person ID", MemberNames = null } } };
      }

      using (var uow = _unitOfWorkFactory.Create())
      {
        Mapper.Map(personModel, person);

        List<ValidationResult> errors = person.Validate().ToList();

        if (errors.Any())
        {
          uow.Undo();
          var result = new ServiceResult<int?> { Errors = Mapper.Map(errors, new List<ValidationResultModel>()) };
          return result;
        }
      }
      return new ServiceResult<int?> { Data = person.Id };
    }

    public void DeletePerson(int id)
    {
      using (_unitOfWorkFactory.Create())
      {
        _peopleRepository.Remove(id);
      }
    }
  }
}
