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
  public class EmailAddressTests : UnitTestBase
  {
    [TestMethod]
    public void CanCreateInstanceOfEmailAddress()
    {
      var emailAddress = new EmailAddress();
      emailAddress.Should().NotBeNull();
    }

    [TestMethod]
    public void NewEmailAddressShouldDefaultToTypeNone()
    {
      var emailAddress = new EmailAddress();
      emailAddress.ContactType.Should().Be(ContactType.None);
    }

    [TestMethod]
    public void NullForEmailAddressTextIsInvalid()
    {
      var emailAddress = new EmailAddress();
      emailAddress.Validate().Count(x => x.MemberNames.Contains("EmailAddressText")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void EmptyStringForEmailAddressTextIsInvalid()
    {
      var emailAddress = new EmailAddress() { EmailAddressText = string.Empty };
      emailAddress.Validate().Count(x => x.MemberNames.Contains("EmailAddressText")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void EmptyEmailAddressHasValidatonMessageAboutMissingAddress()
    {
      var emailAddress = new EmailAddress();
      emailAddress.Validate().Count(x => x.MemberNames.Contains("EmailAddressText")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void EmailAddressWithOwnerIsValid()
    {
      var emailAddress = new EmailAddress() { Owner = new Person() { FirstName = "Imar" } };
      emailAddress.Validate().Count(x => x.MemberNames.Contains("Owner")).Should().Be(0);
    }

    [TestMethod]
    public void EmailAddressWithTypeNoneIsInvalid()
    {
      var emailAddress = new EmailAddress() { EmailAddressText = "imar@spaanjaars.com", Owner = new Person() { FirstName = "Imar" } };
      emailAddress.Validate().Count(x => x.MemberNames.Contains("ContactType")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void InvalidEmailAddressTextShouldInvalidateEmailAddress()
    {
      var emailAddress = new EmailAddress() { EmailAddressText = "imar", Owner = new Person() { FirstName = "Imar" } };
      emailAddress.Validate().Count(x => x.MemberNames.Contains("EmailAddressText")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void ConstructorWithInitialIListAddsToList()
    {
      IList<EmailAddress> initial = new List<EmailAddress> { new EmailAddress(), new EmailAddress() };
      var emailAddresses = new EmailAddresses(initial);
      emailAddresses.Count.Should().Be(2);
    }

    [TestMethod]
    public void ConstructorWithInitialCollectionAddsToList()
    {
      var initial = new EmailAddresses { new EmailAddress(), new EmailAddress() };
      var emailAddresses = new EmailAddresses(initial);
      emailAddresses.Count.Should().Be(2);
    }

    [TestMethod]
    public void OverloadOfAddAddsItemToCollection()
    {
      var emailAddresses = new EmailAddresses();
      const string addressText = "imar@spaanjaars.com";
      emailAddresses.Add(addressText, ContactType.Business);
      emailAddresses.Count.Should().Be(1);
      emailAddresses[0].ContactType.Should().Be(ContactType.Business);
      emailAddresses[0].EmailAddressText.Should().Be(addressText);
    }
  }
}
