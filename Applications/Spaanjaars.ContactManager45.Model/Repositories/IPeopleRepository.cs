using System.Collections.Generic;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Model.Repositories
{
  /// <summary>
  /// Defines the various methods available to work with people in the system.
  /// </summary>
  public interface IPeopleRepository : IRepository<Person, int>
  {
    /// <summary>
    /// Gets a list of all the people whose last name exactly matches the search string.
    /// </summary>
    /// <param name="lastName">The last name that the system should search for.</param>
    /// <returns>An IEnumerable of Person with the matching people.</returns>
    IEnumerable<Person> FindByLastName(string lastName);
  }
}
