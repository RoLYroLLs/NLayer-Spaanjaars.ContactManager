using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Collections;

namespace Spaanjaars.ContactManager45.Tests.Unit
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class PhoneNumberTests : UnitTestBase
  {
    [TestMethod]
    public void CanCreateInstanceOfPhoneNumber()
    {
      var phoneNumber = new PhoneNumber();
      phoneNumber.Should().NotBeNull();
    }

    [TestMethod]
    public void NewPhoneNumberShouldDefaultToTypeNone()
    {
      var phoneNumber = new PhoneNumber();
      phoneNumber.ContactType.Should().Be(ContactType.None);
    }

    [TestMethod]
    public void EmptyPhoneNumberIsInvalid()
    {
      var phoneNumber = new PhoneNumber();
      phoneNumber.Validate().Count().Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void EmptyPhoneNumberHasValidatonMessageAboutMissingNumber()
    {
      var phoneNumber = new PhoneNumber() { Owner = new Person() };
      phoneNumber.Validate().Count(x => x.MemberNames.Contains("Number")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void PhoneNumberWithTypeNoneIsInvalid()
    {
      var phoneNumber = new PhoneNumber() { Number = "555-1234567", Owner = new Person() { FirstName = "Imar" } };
      phoneNumber.Validate().Count(x => x.MemberNames.Contains("ContactType")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void ConstructorWithInitialIListAddsToList()
    {
      IList<PhoneNumber> initial = new List<PhoneNumber> { new PhoneNumber(), new PhoneNumber() };
      var phoneNumbers = new PhoneNumbers(initial);
      phoneNumbers.Count.Should().Be(2);
    }

    [TestMethod]
    public void ConstructorWithInitialCollectionAddsToList()
    {
      var initial = new PhoneNumbers { new PhoneNumber(), new PhoneNumber() };
      var phoneNumbers = new PhoneNumbers(initial);
      phoneNumbers.Count.Should().Be(2);
    }
  }
}
