using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Repositories.EF;
using FluentAssertions;

namespace Spaanjaars.ContactManager45.Tests.Integration
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class EmailAddressTests : IntegrationTestBase
  {
    [TestMethod]
    public void EmailAddressTypeRoundtipsToDatabase()
    {
      int newPersonId = 0;
      ContactType type = ContactType.Personal;
      var address = CreateEmailAddress();
      address.ContactType = type;
      Person person = SimplePeopleTests.CreatePerson();
      person.EmailAddresses.Add(address);

      using (var uwo = new EFUnitOfWorkFactory().Create())
      {
        PeopleRepository peopleRepository = new PeopleRepository();
        peopleRepository.Add(person);
      }
      newPersonId = person.Id;

      newPersonId.Should().BeGreaterThan(0);

      ContactManagerContext context = new ContactManagerContext();
      var check = context.People.Include("EmailAddresses").First(x => x.Id == newPersonId);
      check.Id.Should().Be(newPersonId);
      check.EmailAddresses.First().ContactType.Should().Be(type);
    }

    public static EmailAddress CreateEmailAddress()
    {
      EmailAddress emailAddress = new EmailAddress { EmailAddressText = "you@example.com", ContactType = ContactType.Business };
      return emailAddress;
    }
  }
}
