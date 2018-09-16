using System;
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
  public class AddressTests : IntegrationTestBase
  {
    [TestMethod]
    public void AddressTypeRoundtipsToDatabase()
    {
      int newPersonId = 0;
      var address = CreateAddress(ContactType.Personal);
      Person person = SimplePeopleTests.CreatePerson();
      person.HomeAddress = address;
      address = CreateAddress(ContactType.Business);
      person.WorkAddress = address;

      using (new EFUnitOfWorkFactory().Create())
      {
        PeopleRepository peopleRepository = new PeopleRepository();
        peopleRepository.Add(person);
      }
      newPersonId = person.Id;

      newPersonId.Should().BeGreaterThan(0);

      ContactManagerContext context = new ContactManagerContext();
      var check = context.People.First(x => x.Id == newPersonId);
      check.Id.Should().Be(newPersonId);
      check.HomeAddress.ContactType.Should().Be(ContactType.Personal);
      check.WorkAddress.ContactType.Should().Be(ContactType.Business);
    }

    public static Address CreateAddress(ContactType contactType)
    {
      return new Address("Street", "City", "ZipCode", "Country", contactType);
    }
  }
}
