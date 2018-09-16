using System;
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
  public class PersonTests : UnitTestBase
  {
    [TestMethod]
    public void CanCreateInstanceOfPerson()
    {
      var person = new Person();
      person.Should().NotBeNull();
      person.Id.Should().Be(0);
    }

    [TestMethod]
    public void NewPersonHasInstantiatedEmailAddressCollection()
    {
      var person = new Person();
      person.EmailAddresses.Should().NotBeNull();
      person.EmailAddresses.Should().BeEmpty();
    }

    [TestMethod]
    public void NewPersonHasInstantiatedPhoneNumbersCollection()
    {
      var person = new Person();
      person.PhoneNumbers.Should().NotBeNull();
      person.PhoneNumbers.Should().BeEmpty();
    }

    [TestMethod]
    public void NewPersonShouldDefaultToTypeNone()
    {
      var person = new Person();
      person.Type.Should().Be(PersonType.None);
    }

    [TestMethod]
    public void FirstNameIsRequired()
    {
      var person = new Person();
      person.Validate().Count(x => x.MemberNames.Contains("FirstName")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void LastNameIsRequired()
    {
      var person = new Person();
      person.Validate().Count(x => x.MemberNames.Contains("LastName")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void PersonWithTypeNoneIsInvalid()
    {
      var person = CreatePerson();
      person.Type = PersonType.None;
      person.Validate().Count(x => x.MemberNames.Contains("Type")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void DateOfBirthMustBeOnOBeforeToday()
    {
      var person = CreatePerson();
      person.DateOfBirth = DateTime.Now.AddDays(1);
      person.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().BeGreaterThan(0, "DateOfBirth not found as membername.");
      person.Validate().Count(x => x.ErrorMessage.Contains("Invalid range")).Should().BeGreaterThan(0, "Text invalid range not found.");
    }

    [TestMethod]
    public void DateOfBirthBeforeTodayIsOk()
    {
      var person = CreatePerson();
      person.DateOfBirth = DateTime.Now.AddDays(-1);
      person.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().Be(0, "Yesterday is not considered a valid date.");
    }

    [TestMethod]
    public void TodayIsAValidDateOfBirth()
    {
      var person = CreatePerson();
      person.DateOfBirth = DateTime.Now;
      person.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().Be(0, "Today is not considered a valid date.");
    }

    [TestMethod]
    public void DateOfBirthMustBeLessThan130YearsAgo()
    {
      var person = CreatePerson();
      person.DateOfBirth = DateTime.Now.AddYears(-130).AddDays(-1);
      person.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().BeGreaterThan(0);
      person.Validate().Count(x => x.ErrorMessage.Contains("Invalid range")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void DateOfBirthLessThan130YearsAgoIsValid()
    {
      var person = CreatePerson();
      person.DateOfBirth = DateTime.Now.AddYears(-130).AddDays(1);
      person.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().Be(0);
    }

    [TestMethod]
    public void FirstAndLastNameResultsInFullName()
    {
      var person = new Person { FirstName = "Imar", LastName = "Spaanjaars" };
      person.FullName.Should().Be("Imar Spaanjaars");
    }

    [TestMethod]
    public void EmptyFirstNameReturnsLastName()
    {
      var person = new Person { LastName = "Spaanjaars" };
      person.FullName.Should().Be("Spaanjaars");
    }

    [TestMethod]
    public void EmptyLastNameReturnsFirstName()
    {
      var person = new Person { FirstName = "Imar" };
      person.FullName.Should().Be("Imar");
    }

    [TestMethod]
    public void AllEmptyReturnsEmpty()
    {
      var person = new Person();
      person.FullName.Should().Be(string.Empty);
    }

    [TestMethod]
    public void TwoPeopleWithSameIdAreTheSame()
    {
      var person1 = new Person { Id = 1, FirstName = "Imar", LastName = "Spaanjaars" };
      var person2 = new Person { Id = 1, FirstName = "Imar", LastName = "Spaanjaars" };
      (person1 == person2).Should().BeTrue();
    }

    [TestMethod]
    public void CanAddEmailAddressToNewPerson()
    {
      var person = new Person();
      person.EmailAddresses.Add(new EmailAddress());
      person.EmailAddresses.Count.Should().Be(1);
    }

    [TestMethod]
    public void CanAddPhoneNumberToNewPerson()
    {
      var person = new Person();
      person.PhoneNumbers.Add(new PhoneNumber());
      person.PhoneNumbers.Count.Should().Be(1);
    }

    [TestMethod]
    public void NewPersonHasInstantiatedWorkAddress()
    {
      var person = new Person();
      person.WorkAddress.Should().NotBeNull();
    }

    [TestMethod]
    public void NewPersonHasInstantiatedHomeAddress()
    {
      var person = new Person();
      person.HomeAddress.Should().NotBeNull();
    }

    [TestMethod]
    public void PersonDotValidateDetectsInvalidPhoneNumber()
    {
      var person = CreatePerson();
      person.PhoneNumbers.Add("", ContactType.None);
      var errors = person.Validate();
      errors.Should().Contain(x => x.ErrorMessage.Contains("The Number field is required"));
    }

    [TestMethod]
    public void PersonDotValidateDetectsMissingPhonenumberType()
    {
      var person = CreatePerson();
      person.PhoneNumbers.Add("(555)1234567", ContactType.None);
      var errors = person.Validate();
      errors.Should().Contain(x => x.ErrorMessage.Contains("ContactType can't be None"));
    }

    [TestMethod]
    public void PersonDotValidateDetectsInvalidEmailAddress()
    {
      var person = CreatePerson();
      person.EmailAddresses.Add("", ContactType.None);
      var errors = person.Validate();
      errors.Should().Contain(x => x.ErrorMessage.Contains("The EmailAddressText field is required"));
    }

    [TestMethod]
    public void PersonDotValidateDetectsMissingEmailAddressType()
    {
      var person = CreatePerson();
      person.EmailAddresses.Add("test@example.com", ContactType.None);
      var errors = person.Validate();
      errors.Should().Contain(x => x.ErrorMessage.Contains("ContactType can't be None"));
    }

    [TestMethod]
    public void PersonDotValidateDetectsInvalidHomeAddress()
    {
      var person = CreatePerson();
      person.HomeAddress = new Address("Street", null, null, null, ContactType.Personal);
      var errors = person.Validate();
      errors.Should().Contain(x => x.ErrorMessage.Contains("City can't be null or empty"));
    }

    [TestMethod]
    public void PersonDotValidateDetectsInvalidWorkAddress()
    {
      var person = CreatePerson();
      person.WorkAddress = new Address("Street", null, null, null, ContactType.Personal);
      var errors = person.Validate();
      errors.Should().Contain(x => x.ErrorMessage.Contains("City can't be null or empty"));
    }

    [TestMethod]
    public void ConstructorWithInitialIListAddsToList()
    {
      IList<Person> initial = new List<Person> { new Person(), new Person() };
      var people = new People(initial);
      people.Count.Should().Be(2);
    }

    [TestMethod]
    public void ConstructorWithInitialCollectionAddsToList()
    {
      var initial = new People { new Person(), new Person() };
      var people = new People(initial);
      people.Count.Should().Be(2);
    }

    [TestMethod]
    public void ValidateOnPeopleDetectsInvalidPeople()
    {
      var people = new People {new Person {FirstName = "Imar"}, new Person {LastName = "Spaanjaars"}};
      var result = people.Validate().ToList();
      result.Should().Contain(x => x.ErrorMessage.Contains("The LastName field is required")); // for Person 1
      result.Should().Contain(x => x.ErrorMessage.Contains("The FirstName field is required")); // for Person 2
    }


    private static Person CreatePerson()
    {
      return new Person { FirstName = "Imar", LastName = "Spaanjaars", Type = PersonType.Friend, DateOfBirth = DateTime.Now.AddYears(-20) };
    }




  }
}
