using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;
using System.Linq.Expressions;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Mvc
{
  [ExcludeFromCodeCoverage]
  internal class FakePeopleRepository : IPeopleRepository
  {
    public IQueryable<Person> FindAll(params Expression<Func<Person, object>>[] includeProperties)
    {
      var temp = new List<Person>();
      var youngestPerson = new DateTime(2007, 12, 1);
      for (int i = 0; i < 23; i++)
      {
        temp.Add(new Person { FirstName = i.ToString(), LastName = i.ToString(), DateOfBirth = youngestPerson.AddDays(-i), Id = i + 1 });
      }
      temp.Insert(11, new Person { FirstName = "Youngest", LastName = "Youngest Lastname", DateOfBirth = youngestPerson, Id = 24 });
      return temp.AsQueryable();
    }

    public void Add(Person entity)
    {
      throw new NotImplementedException();
    }

    public void Remove(Person entity)
    {
      throw new NotImplementedException();
    }

    public Person FindById(int id, params Expression<Func<Person, object>>[] includeProperties)
    {
      return FindAll().SingleOrDefault(x => x.Id == id);
    }

    public void Remove(int id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Person> FindByLastName(string lastName)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Person> FindAll(Expression<Func<Person, bool>> predicate, params Expression<Func<Person, object>>[] includeProperties)
    {
      throw new NotImplementedException();
    }
  }
}
