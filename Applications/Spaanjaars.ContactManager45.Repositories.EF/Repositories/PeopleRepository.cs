using System.Linq;
using System.Collections.Generic;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Model.Repositories;

namespace Spaanjaars.ContactManager45.Repositories.EF
{
  /// <summary>
  /// A concrete repository to work with people in the system.
  /// </summary>
  public class PeopleRepository : Repository<Person>, IPeopleRepository
  {
    /// <summary>
    /// Gets a list of all the people whose last name exactly matches the search string.
    /// </summary>
    /// <param name="lastName">The last name that the system should search for.</param>
    /// <returns>An IEnumerable of Person with the matching people.</returns>
    public IEnumerable<Person> FindByLastName(string lastName)
    {
      return DataContextFactory.GetDataContext().Set<Person>().Where(x => x.LastName == lastName);
    }
  }
}


