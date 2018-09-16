using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Collections;

namespace Spaanjaars.ContactManager45.Tests.Unit
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class CollectionBaseTests : UnitTestBase
  {
    [TestMethod]
    public void NewCollectionUsingNewListsAddsValues()
    {
      var collection = new IntCollection(new List<int> { 1, 2, 3 });
      collection.Count.Should().Be(3);
    }

    [TestMethod]
    public void NewCollectionUsingExistingCollectionAddsValues()
    {
      var collection1 = new IntCollection(new List<int> { 1, 2, 3 });
      var collection2 = new IntCollection(collection1);
      collection2.Count.Should().Be(3);
    }

    [TestMethod]
    public void UsingAddRangeAddsValues()
    {
      var collection1 = new IntCollection(new List<int> { 1, 2, 3 });
      var collection2 = new IntCollection();
      collection2.AddRange(collection1);
      collection2.Count.Should().Be(3);
    }

    [TestMethod]
    public void SortPeopleWithSpecifiedComparerSortsCorrectly()
    {
      var people = new People();
      people.Add(new Person { FirstName = "John", LastName = "Doe" });
      people.Add(new Person { FirstName = "Imar", LastName = "Spaanjaars" });
      people.Add(new Person { FirstName = "Jane", LastName = "Doe" });

      people.Sort(new PersonComparer());

      people[0].FullName.Should().Be("Imar Spaanjaars");
      people[1].FullName.Should().Be("Jane Doe");
      people[2].FullName.Should().Be("John Doe");
    }

    [TestMethod]
    public void SortIntsSorts()
    {
      var ints = new IntCollection { 3, 2, 1 };
      ints.Sort();
      ints[0].Should().Be(1);
      ints[1].Should().Be(2);
      ints[2].Should().Be(3);
    }

    [TestMethod]
    public void AddRangeThrowsWhenCollectionIsNull()
    {
      Action act = () =>
        {
          var collection = new IntCollection();
          collection.AddRange(null);
        };
      act.ShouldThrow<ArgumentNullException>().WithMessage("collection is null", ComparisonMode.Substring);
    }


  }

  [ExcludeFromCodeCoverage]
  internal class IntCollection : CollectionBase<int>
  {
    public IntCollection()
    { }

    public IntCollection(IList<int> initialList)
      : base(initialList)
    { }

    public IntCollection(CollectionBase<int> initialList)
      : base(initialList)
    { }
  }

  [ExcludeFromCodeCoverage]
  public class PersonComparer : IComparer<Person>
  {
    public int Compare(Person x, Person y)
    {
      return x.FullName.CompareTo(y.FullName);
    }
  }

}
