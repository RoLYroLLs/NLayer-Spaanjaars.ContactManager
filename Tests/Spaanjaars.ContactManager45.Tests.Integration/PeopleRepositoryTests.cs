using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Repositories.EF;

namespace Spaanjaars.ContactManager45.Tests.Integration
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class PeopleRepositoryTests : IntegrationTestBase
  {
    [TestMethod]
    public void FindByFindsPerson()
    {
      var person = CreatePerson();
      var repository = new PeopleRepository();
      using (new EFUnitOfWorkFactory().Create())
      {
        repository.Add(person);
      }
      person.Id.Should().BePositive();

      var check = repository.FindById(person.Id);

      check.Id.Should().Be(person.Id);
    }

    [TestMethod]
    public void UnknownIdOnFindByReturnsNull()
    {
      var repository = new PeopleRepository();
      repository.FindById(-1).Should().BeNull();
    }

    [TestMethod]
    public void FindByLastNameReturnsCorrectPeople()
    {
      string lastName = Guid.NewGuid().ToString().Substring(0, 25);
      var person1 = CreatePerson();
      var person2 = CreatePerson();
      person1.LastName = lastName;
      person2.LastName = lastName;
      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.Add(person1);
        repository.Add(person2);
      }

      using (new EFUnitOfWorkFactory().Create(true))
      {
        var repository = new PeopleRepository();
        var peopleWithLastName = repository.FindByLastName(lastName);
        peopleWithLastName.Count().Should().Be(2);
      }
    }


    [TestMethod]
    public void FindAllWithPredicateSearchesCorrectlyForLastNameAndType()
    {
      string lastName = Guid.NewGuid().ToString().Substring(0, 25);
      var person1 = CreatePerson();
      var person2 = CreatePerson();
      person1.LastName = lastName;
      person1.Type = PersonType.Colleague;
      person2.LastName = lastName;
      person2.Type = PersonType.Friend;
      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.Add(person1);
        repository.Add(person2);
      }

      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.FindAll(x => x.LastName == lastName).Count().Should().Be(2);
        repository.FindAll(x => x.LastName == lastName && x.Type == PersonType.Colleague).Count().Should().Be(1);
      }
    }

    [TestMethod]
    public void FindAllWithPredicateSearchesCorrectly()
    {
      var person1 = CreatePerson();
      var person2 = CreatePerson();
      var person3 = CreatePerson();
      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.Add(person1);
        repository.Add(person2);
        repository.Add(person3);
      }

      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        var oneAndTwo = repository.FindAll(x => x.Id == person1.Id || x.Id == person2.Id).ToList();
        oneAndTwo.Count().Should().Be(2);
        oneAndTwo.First(x => x.Id == person1.Id).Should().NotBeNull();
        oneAndTwo.First(x => x.Id == person2.Id).Should().NotBeNull();
        oneAndTwo.FirstOrDefault(x => x.Id == person3.Id).Should().BeNull();
      }
    }

    [TestMethod]
    public void FindAllWithPredicateEagerLoadsCorrectly()
    {
      var person1 = CreatePerson();
      person1.EmailAddresses.Add("1@example.com", ContactType.Personal);
      person1.EmailAddresses.Add("2@example.com", ContactType.Business);
      person1.PhoneNumbers.Add("555-123", ContactType.Personal);
      person1.PhoneNumbers.Add("555-456", ContactType.Business);
      var person2 = CreatePerson();
      var person3 = CreatePerson();
      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.Add(person1);
        repository.Add(person2);
        repository.Add(person3);
      }

      using (new EFUnitOfWorkFactory().Create(true))
      {
        var repository = new PeopleRepository();
        var oneAndTwo = repository.FindAll(x => x.Id == person1.Id || x.Id == person2.Id, x => x.EmailAddresses).ToList();
        oneAndTwo.Count().Should().Be(2);
        oneAndTwo.First(x => x.Id == person1.Id).Should().NotBeNull();
        oneAndTwo.First(x => x.Id == person2.Id).Should().NotBeNull();
        oneAndTwo.FirstOrDefault(x => x.Id == person3.Id).Should().BeNull();
        oneAndTwo.First(x => x.Id == person1.Id).EmailAddresses.Count.Should().Be(2);
        oneAndTwo.First(x => x.Id == person1.Id).PhoneNumbers.Count.Should().Be(0);
      }
    }

    [TestMethod]
    public void DeletingPersonDeletesPhoneNumbers()
    {
      string number1 = Guid.NewGuid().ToString().Substring(0, 25);
      string number2 = Guid.NewGuid().ToString().Substring(0, 25);
      string sql = string.Format("SELECT * FROM PhoneNumbers WHERE Number = '{0}'", number1);

      var person = CreatePerson();
      person.PhoneNumbers.Add(number1, ContactType.Personal);
      person.PhoneNumbers.Add(number2, ContactType.Personal);

      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.Add(person);
      }

      CheckIfExists(sql).Should().BeTrue();
      int personId = person.Id;
      personId.Should().BeGreaterThan(0);
      var checkRepository = new PeopleRepository();
      using (new EFUnitOfWorkFactory().Create(true))
      {
        checkRepository.Remove(personId);
      }
      CheckIfExists(sql).Should().BeFalse();
    }

    [TestMethod]
    public void ClearingPhoneNumbersCollectionDeletesPhoneNumbers()
    {
      string number1 = Guid.NewGuid().ToString().Substring(0, 25);
      string number2 = Guid.NewGuid().ToString().Substring(0, 25);
      string sql = string.Format("SELECT * FROM PhoneNumbers WHERE Number = '{0}'", number1);

      var person = CreatePerson();
      person.PhoneNumbers.Add(number1, ContactType.Personal);
      person.PhoneNumbers.Add(number2, ContactType.Personal);

      using (new EFUnitOfWorkFactory().Create())
      {
        var repository = new PeopleRepository();
        repository.Add(person);
      }

      CheckIfExists(sql).Should().BeTrue();
      int personId = person.Id;
      personId.Should().BeGreaterThan(0);
      var checkRepository = new PeopleRepository();
      using (new EFUnitOfWorkFactory().Create(true))
      {
        var checkPerson = checkRepository.FindById(personId, x => x.PhoneNumbers);
        checkPerson.PhoneNumbers.Clear();
      }
      CheckIfExists(sql).Should().BeFalse();
    }

    private static Person CreatePerson()
    {
      var person = new Person
                    {
                      FirstName = "Imar",
                      LastName = "Spaanjaars",
                      DateOfBirth = new DateTime(1971, 8, 9),
                      Type = PersonType.Friend
                    };
      person.HomeAddress = AddressTests.CreateAddress(ContactType.Personal);
      person.WorkAddress = AddressTests.CreateAddress(ContactType.Business);
      return person;
    }


    private bool CheckIfExists(string sql)
    {
      var connection = new SqlConnection(new ContactManagerContext().Database.Connection.ConnectionString);
      var command = new SqlCommand(sql, connection);
      connection.Open();
      var reader = command.ExecuteReader();
      bool result = reader.Read();
      reader.Close();
      connection.Close();
      return result;
    }
  }
}
