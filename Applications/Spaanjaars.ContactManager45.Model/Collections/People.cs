using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spaanjaars.ContactManager45.Model.Collections
{
  /// <summary>
  /// Represents a collection of People instances in the system.
  /// </summary>
  public class People : CollectionBase<Person>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="People"/> class.
    /// </summary>
    public People() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="People"/> class.
    /// </summary>
    /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
    public People(IList<Person> initialList) : base(initialList) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="People"/> class.
    /// </summary>
    /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
    public People(CollectionBase<Person> initialList) : base(initialList) { }

    /// <summary>
    /// Validates the current collection by validating each individual item in the collection.
    /// </summary>
    /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
    public IEnumerable<ValidationResult> Validate()
    {
      var errors = new List<ValidationResult>();
      foreach (var person in this)
      {
        errors.AddRange(person.Validate());
      }
      return errors;
    }
  }
}
