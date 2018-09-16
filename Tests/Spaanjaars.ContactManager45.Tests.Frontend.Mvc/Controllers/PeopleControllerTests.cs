using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Web.Mvc.Controllers;
using Spaanjaars.ContactManager45.Web.Mvc.Models;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Mvc.Controllers
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class PeopleControllerTests : PresentationTestBase
  {
    [TestMethod]
    public void Index()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Index() as ViewResult;
      ((PagerModel<DisplayPerson>)result.Model).Data.Should().BeAssignableTo<IEnumerable<DisplayPerson>>();
    }

    [TestMethod]
    public void IndexSortsAndPagesCorrectly()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Index(1, "DateOfBirth", "DESC") as ViewResult;
      var model = ((PagerModel<DisplayPerson>)result.Model);
      model.Data.Count().Should().Be(10);
      model.Data.First().DateOfBirth.Year.Should().Be((2007));
      model.Data.First().DateOfBirth.Month.Should().Be((12));
      model.Data.First().DateOfBirth.Day.Should().Be((1));
    }

    [TestMethod]
    public void IndexSortsByFullNameAndPagesCorrectly()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Index(1, "FullName") as ViewResult;
      var model = ((PagerModel<DisplayPerson>)result.Model);
      model.Data.Count().Should().Be(10);
      model.Data.First().FullName.Should().Be("0 0");
    }

    [TestMethod]
    public void Edit()
    {
      const int personId = 7;
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Edit(personId) as ViewResult;
      result.Model.Should().BeOfType<CreateAndEditPerson>();
      ((CreateAndEditPerson)result.Model).Id.Should().Be(personId);
    }

    [TestMethod]
    public void DetailsGet()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Details(24) as ViewResult;
      var model = (DisplayPerson)result.Model;
      model.FullName.Should().Be("Youngest Youngest Lastname");
    }

    [TestMethod]
    public void DetailsReturns404ForUnknownPersonId()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Details(-1) as ActionResult;
      result.Should().BeOfType<HttpNotFoundResult>();
    }

    [TestMethod]
    public void EditReturns404ForUnknownPersonId()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Edit(-1) as ActionResult;
      result.Should().BeOfType<HttpNotFoundResult>();
    }

    [TestMethod]
    public void CreateReturnsView()
    {
      var controller = new PeopleController(new FakePeopleRepository(), null);
      var result = controller.Create() as ViewResult;
      result.Should().NotBeNull();
    }
  }
}
