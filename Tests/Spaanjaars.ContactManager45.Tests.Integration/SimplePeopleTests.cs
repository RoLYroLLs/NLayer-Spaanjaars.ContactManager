using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.Infrastructure;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Repositories.EF;
using System;

namespace Spaanjaars.ContactManager45.Tests.Integration
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class SimplePeopleTests : IntegrationTestBase
  {
    [TestMethod]
    public void CanGetBasicPerson()
    {
      Person person = CreatePerson();
      PeopleRepository repository = new PeopleRepository();
      using (IUnitOfWork unitOfWork = new EFUnitOfWorkFactory().Create())
      {
        repository.Add(person);
      }
      PeopleRepository repositoryConfirm = new PeopleRepository();
      Person personConfirm = repositoryConfirm.FindById(person.Id);
      personConfirm.Id.Should().Be(person.Id);

      var friendsWithATattoo = repository.FindAll(
            x => x.FirstName == "Goo" && x.Type == PersonType.Friend);

    }

    [TestMethod]
    public void EnumOnPersonTypeRountripsToDatabase()
    {
      Person person = CreatePerson();
      person.Type = PersonType.Colleague;
      using (IUnitOfWork unitOfWork = new EFUnitOfWorkFactory().Create())
      {
        PeopleRepository repository = new PeopleRepository();
        repository.Add(person);
      }
      PeopleRepository repoConfirm = new PeopleRepository();
      Person personCheck = repoConfirm.FindById(person.Id);
      Assert.AreEqual(PersonType.Colleague, personCheck.Type);
    }

    [TestMethod]
    public void FindByWithIncludeReturnsOnlyIncludedAndNotOtherProperties()
    {
      var phoneNumber1 = new PhoneNumber { ContactType = ContactType.Business, Number = "555-12345678" };
      var phoneNumber2 = new PhoneNumber { ContactType = ContactType.Business, Number = "555-12345678" };
      var emailAddress1 = EmailAddressTests.CreateEmailAddress();
      var emailAddress2 = EmailAddressTests.CreateEmailAddress();
      Person person = CreatePerson();
      person.PhoneNumbers.Add(phoneNumber1);
      person.PhoneNumbers.Add(phoneNumber2);
      person.EmailAddresses.Add(emailAddress1);
      person.EmailAddresses.Add(emailAddress2);

      using (var uow = new EFUnitOfWorkFactory().Create())
      {
        var peopleRepository = new PeopleRepository();
        peopleRepository.Add(person);
      }

      using (var uow = new EFUnitOfWorkFactory().Create(true))
      {
        var peopleRepository = new PeopleRepository();
        var check = peopleRepository.FindById(person.Id, x => x.PhoneNumbers);
        check.PhoneNumbers.Count.Should().Be(2);
        check.EmailAddresses.Count.Should().Be(0);
      }
    }

    public static Person CreatePerson()
    {
      Person person = new Person() { FirstName = "Imar", LastName = "Spaanjaars", Type = PersonType.Colleague, DateOfBirth = DateTime.Now.AddYears(-20) };
      person.HomeAddress = AddressTests.CreateAddress(ContactType.Personal);
      person.WorkAddress = AddressTests.CreateAddress(ContactType.Business);
      return person;
    }
  }
}
