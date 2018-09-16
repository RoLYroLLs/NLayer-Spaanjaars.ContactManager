using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Repositories.EF;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Integration
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class ContactManagerContextTests : IntegrationTestBase
  {
    [TestMethod]
    public void CanAddPersonUsingContactManagerContext()
    {
      var person = new Person { FirstName = "Imar", LastName = "Spaanjaars", DateOfBirth = new DateTime(1971, 8, 9), DateCreated = DateTime.Now, DateModified = DateTime.Now, Type = PersonType.Colleague, HomeAddress = AddressTests.CreateAddress(ContactType.Personal), WorkAddress = AddressTests.CreateAddress(ContactType.Business) };
      var context = new ContactManagerContext();
      context.People.Add(person);
      context.SaveChanges();
    }

    [TestMethod]
    public void CanExecuteQueryAgainstDataContext()
    {
      string lastName = Guid.NewGuid().ToString().Substring(0, 25);
      var context = DataContextFactory.GetDataContext();
      var person = new Person { FirstName = "Imar", LastName = lastName, DateOfBirth = new DateTime(1971, 8, 9), DateCreated = DateTime.Now, DateModified = DateTime.Now, Type = PersonType.Colleague, HomeAddress = AddressTests.CreateAddress(ContactType.Personal), WorkAddress = AddressTests.CreateAddress(ContactType.Business) };
      context.People.Add(person);
      context.SaveChanges();

      var personCheck = context.People.First(x => x.LastName == lastName);
      personCheck.Should().NotBeNull();
    }

    [TestMethod]
    public void ValidationErrorsThrowModelValidationException()
    {
      var uow = new EFUnitOfWorkFactory().Create();
      Action act = () =>
        {
          var repo = new PeopleRepository();
          repo.Add(new Person());
          uow.Commit(true);
        };
      act.ShouldThrow<ModelValidationException>().WithMessage("The FirstName field is required", ComparisonMode.Substring);
      uow.Undo();
    }
  }
}
