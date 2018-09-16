namespace Spaanjaars.ContactManager45.Model
{
  /// <summary>
  /// This interface is used to mark the owner of an object.
  /// </summary>
  public interface IHasOwner
  {
    /// <summary>
    /// The Person instance this object belongs to.
    /// </summary>
    Person Owner { get; set; }
  }
}
