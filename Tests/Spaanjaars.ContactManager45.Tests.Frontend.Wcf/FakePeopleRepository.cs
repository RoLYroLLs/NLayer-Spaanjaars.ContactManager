using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;

namespace Spaanjaars.ContactManager45.Tests.Frontend.Wcf
{
  [ExcludeFromCodeCoverage]
  internal class FakePeopleRepository : IPeopleRepository
  {
    private IQueryable<Person> list;

    public Person InsertedPerson { get; private set; }
    public Person DeletedPerson { get; private set; }

    public IQueryable<Person> FindAll(params Expression<Func<Person, object>>[] includeProperties)
    {
      if (list != null)
      {
        return list;
      }
      var temp = new List<Person>();
      var youngestPerson = new DateTime(2007, 12, 1);
      for (int i = 0; i < 23; i++)
      {
        temp.Add(new Person { FirstName = i.ToString(), LastName = i.ToString(), DateOfBirth = youngestPerson.AddDays(-i), Id = i + 1, Type = PersonType.Friend });
      }
      temp.Insert(11, new Person { FirstName = "Youngest", LastName = "Youngest Lastname", DateOfBirth = youngestPerson, Id = 24, Type = PersonType.Family });
      list = temp.AsQueryable();
      return list;
    }

    public void Add(Person entity)
    {
      InsertedPerson = entity;
      entity.Id = 123;
    }

    public void Remove(Person entity)
    {
      DeletedPerson = entity;
    }

    public void Remove(int id)
    {
      Remove(FindById(id));
    }

    public Person FindById(int id, params Expression<Func<Person, object>>[] includeProperties)
    {
      return FindAll().SingleOrDefault(x => x.Id == id);
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
