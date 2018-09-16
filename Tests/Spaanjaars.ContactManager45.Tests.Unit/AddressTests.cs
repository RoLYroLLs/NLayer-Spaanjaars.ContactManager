using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Tests.Unit
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class AddressTests : UnitTestBase
  {
    [TestMethod]
    public void TwoIdenticalAddressesShouldBeTheSame()
    {
      var address1 = new Address ( "Street", "City", "ZipCode", "Country", ContactType.Business);
      var address2 = new Address("Street", "City", "ZipCode", "Country", ContactType.Business);
      (address1 == address2).Should().BeTrue();
    }

    [TestMethod]
    public void DifferentAddressesShouldNotBeTheSame()
    {
      var address1 = new Address("Some other street", "City", "ZipCode", "Country", ContactType.Business);
      var address2 = new Address("Street", "City", "ZipCode", "Country", ContactType.Business);
      (address1 != address2).Should().BeTrue();
    }

    [TestMethod]
    public void CanCreateInstanceOfAddress()
    {
      var address = new Address(null, null, null, null, ContactType.Business);
      address.Should().NotBeNull();
    }

    [TestMethod]
    public void EmptyAddressIsNull()
    {
      var address = new Address(null, null, null, null, ContactType.Business);
      address.IsNull.Should().BeTrue();
    }

    [TestMethod]
    public void AddressWithValuesIsNotNull()
    {
      var address = new Address("Street", "City", "ZipCode", "Country", ContactType.Business);
      address.IsNull.Should().BeFalse();
    }

    [TestMethod]
    public void AddressWithSomeValuesIsNotNull()
    {
      var address = new Address(null, null, "ZipCode", "Country", ContactType.Business);
      address.IsNull.Should().BeFalse();
    }

    [TestMethod]
    public void NewAddressShouldBeOfCorrectType()
    {
      var address = new Address(null, null, null, null, ContactType.Business);
      address.ContactType.Should().Be(ContactType.Business);
    }

    [TestMethod]
    public void EmptyAddressIsNotInvalid()
    {
      var address = new Address(null, null, null, null, ContactType.Business);
      address.Validate().Count().Should().Be(0);
    }

    [TestMethod]
    public void PartialAddressHasValidatonMessageAboutMissingStreet()
    {
      var address = new Address(null, null, null, "Country", ContactType.Business);
      address.Validate().Count(x => x.MemberNames.Contains("Street")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void PartialAddressHasValidatonMessageAboutMissingZipCode()
    {
      var address = new Address(null, null, null, "Country", ContactType.Business);
      address.Validate().Count(x => x.MemberNames.Contains("ZipCode")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void PartialAddressHasValidatonMessageAboutMissingCity()
    {
      var address = new Address(null, null, null, "Country", ContactType.Business);
      address.Validate().Count(x => x.MemberNames.Contains("City")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void PartialAddressHasValidatonMessageAboutMissingCountry()
    {
      var address = new Address(null, "City", null, null, ContactType.Business);
      address.Validate().Count(x => x.MemberNames.Contains("Country")).Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void AddressWithTypeNoneIsInvalid()
    {
      var address = new Address("Street", "City", "ZipCode", "Country", ContactType.None);
      address.Validate().Count(x => x.MemberNames.Contains("ContactType")).Should().BeGreaterThan(0);
    }
  }
}
