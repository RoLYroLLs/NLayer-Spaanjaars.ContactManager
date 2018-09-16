using AutoMapper;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager.Import
{
  /// <summary>
  /// Mapping class for mapping ImportPerson to Person.
  /// </summary>
  public static class AutoMapperConfig
  {
    /// <summary>
    /// Sets up the AutoMapper mappings. Ignores properties like EmailAddresses and PhoneNumbers 
    /// and sets up specific actions for the two addresses.
    /// </summary>
    public static void Start()
    {
      Mapper.CreateMap<ImportPerson, Person>()
          .ForMember(x => x.Id, x => x.Ignore())
          .ForMember(x => x.DateCreated, x => x.Ignore())
          .ForMember(x => x.DateModified, x => x.Ignore())
          .ForMember(x => x.EmailAddresses, x => x.Ignore())
          .ForMember(x => x.PhoneNumbers, x => x.Ignore())
          .ForMember(x => x.HomeAddress, x => x.ResolveUsing(ip => new Address(ip.Address, ip.City, ip.Zip, ip.Country, ContactType.Personal)))
          .ForMember(x => x.WorkAddress, x => x.ResolveUsing(ip => new Address(ip.Address2, ip.City2, ip.Zip2, ip.Country2, ContactType.Business)));
      Mapper.AssertConfigurationIsValid();
    }
  }
}
