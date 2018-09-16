using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Web.Wcf;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class AutoMapperTests : ServiceTestBase
  {
    [TestMethod]
    public void AllMappingIsValid()
    {
      AutoMapperConfig.Start(); // Should crash when configuration is invalid.
    }

    [TestMethod]
    public void NullValueInSourceDoesNotOverwriteTarget()
    {
      var personModel = new PersonModel { FirstName = "Imar", LastName = "Spaanjaars" };
      var person = new Person { FirstName = "Old", LastName = "Old" };
      person.HomeAddress = new Address("Street", "City", "ZipCode", "Country", ContactType.Business);
      Mapper.Map(personModel, person);
      person.FirstName.Should().Be("Imar");    // Taken from the model; should overwrite settings
      person.HomeAddress.Street.Should().Be("Street"); // Taken from person, null value from model should have been ignored with the Condition and IsSourceValueNull in AutoMapper config.
    }

    [TestMethod]
    public void NonNullValueInSourceDoesOverwritesTarget()
    {
      var personModel = new PersonModel { FirstName = "Imar", LastName = "Spaanjaars", HomeAddress = new AddressModel {City = "City from model"}};
      var person = new Person { FirstName = "Old", LastName = "Old", HomeAddress = new Address("Street", "City", "ZipCode", "Country", ContactType.Business) };
      Mapper.Map(personModel, person);
      person.FirstName.Should().Be("Imar");    // Taken from the model; should overwrite settings
      person.HomeAddress.Street.Should().BeNull(); // Taken from personModel. 
      person.HomeAddress.City.Should().Be("City from model"); // Taken from personModel. 
    }
  }
}
