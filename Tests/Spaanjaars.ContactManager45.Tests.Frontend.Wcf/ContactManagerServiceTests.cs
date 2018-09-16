using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Web.Wcf;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class ContactManagerServiceTests : ServiceTestBase
  {
    [TestMethod]
    public void ContactManagerServiceRequiresRepository()
    {
      Action act = () => new ContactManagerService(null, new FakeUnitOfWorkFactory());
      act.ShouldThrow<ArgumentNullException>().WithMessage("peopleRepository is null", ComparisonMode.Substring);
    }

    [TestMethod]
    public void ContactManagerServiceRequiresUnitOfWorkFactory()
    {
      Action act = () => new ContactManagerService(new FakePeopleRepository(), null);
      act.ShouldThrow<ArgumentNullException>().WithMessage("unitOfWorkFactory is null", ComparisonMode.Substring);
    }

    [TestMethod]
    public void ContactManagerServiceRequiresRepositoryAndUnitOfWorkFactory()
    {
      Action act = () => new ContactManagerService(new FakePeopleRepository(), new FakeUnitOfWorkFactory());
      act.ShouldNotThrow();
    }

    [TestMethod]
    public void GetPersonByIdReturnsCorrectPersonModel()
    {
      var service = new ContactManagerService(new FakePeopleRepository(), new FakeUnitOfWorkFactory());
      PersonModel peron = service.GetPerson(24);
      peron.LastName.Should().Be("Youngest Lastname");
      peron.Id.Should().Be(24);
    }

    [TestMethod]
    public void UnknownIdReturnsNull()
    {
      var service = new ContactManagerService(new FakePeopleRepository(), new FakeUnitOfWorkFactory());
      PersonModel peron = service.GetPerson(-1);
      peron.Should().BeNull();
    }

    [TestMethod]
    public void InsertPersonInsertsValidPerson()
    {
      var fakePeopleRepository = new FakePeopleRepository();
      var service = new ContactManagerService(fakePeopleRepository, new FakeUnitOfWorkFactory());
      var peronModel = new PersonModel { DateOfBirth = DateTime.Now.AddDays(-1), FirstName = "Imar", LastName = "Spaanjaars" };
      var result = service.InsertPerson(peronModel);
      fakePeopleRepository.InsertedPerson.FirstName.Should().Be("Imar");
      result.Errors.Should().BeEmpty();
      result.Data.HasValue.Should().BeTrue();
      result.Data.Value.Should().Be(123);
    }

    [TestMethod]
    public void InsertPersonRejectsInvalidPerson()
    {
      var fakePeopleRepository = new FakePeopleRepository();
      var service = new ContactManagerService(fakePeopleRepository, new FakeUnitOfWorkFactory());
      var peronModel = new PersonModel { DateOfBirth = DateTime.Now.AddDays(+1), FirstName = "Imar", LastName = "Spaanjaars" };
      var result = service.InsertPerson(peronModel);
      fakePeopleRepository.InsertedPerson.Should().BeNull();
      result.Errors.Should().Contain(x => x.ErrorMessage.ToLower().Contains("dateofbirth"));
      result.Data.HasValue.Should().BeFalse();
    }

    [TestMethod]
    public void UpdatePersonUpdatesCorrectPerson()
    {
      var fakePeopleRepository = new FakePeopleRepository();
      var service = new ContactManagerService(fakePeopleRepository, new FakeUnitOfWorkFactory());
      var peronModel = new PersonModel { Id = 24, DateOfBirth = DateTime.Now.AddDays(-1), FirstName = "New First Name", LastName = "New Last Name" };
      var result = service.UpdatePerson(peronModel);
      fakePeopleRepository.FindById(24).LastName.Should().Be("New Last Name");
      result.Errors.Should().BeEmpty();
      result.Data.Value.Should().Be(24);
    }

    [TestMethod]
    public void UpdatePersonRejectsInvalidPerson()
    {
      var fakePeopleRepository = new FakePeopleRepository();
      var service = new ContactManagerService(fakePeopleRepository, new FakeUnitOfWorkFactory());
      var peronModel = new PersonModel { Id = 24, DateOfBirth = DateTime.Now.AddDays(-1), FirstName = "", LastName = "" };
      var result = service.UpdatePerson(peronModel);
      result.Errors.Should().Contain(x => x.ErrorMessage.ToLower().Contains("lastname field is required"));
      result.Data.HasValue.Should().BeFalse();
    }

    [TestMethod]
    public void CantUpdateNonExistentPerson()
    {
      var fakePeopleRepository = new FakePeopleRepository();
      var service = new ContactManagerService(fakePeopleRepository, new FakeUnitOfWorkFactory());
      var peronModel = new PersonModel { Id = -1, DateOfBirth = DateTime.Now.AddDays(-1), FirstName = "Something", LastName = "Something Else" };
      var result = service.UpdatePerson(peronModel);
      result.Errors.Should().Contain(x => x.ErrorMessage.ToLower().Contains("unknown person id"));
    }

    [TestMethod]
    public void DeleteDeletesCorrectPerson()
    {
      var fakePeopleRepository = new FakePeopleRepository();
      var service = new ContactManagerService(fakePeopleRepository, new FakeUnitOfWorkFactory());
      service.DeletePerson(24);
      fakePeopleRepository.DeletedPerson.Id.Should().Be(24);
    }
  }
}