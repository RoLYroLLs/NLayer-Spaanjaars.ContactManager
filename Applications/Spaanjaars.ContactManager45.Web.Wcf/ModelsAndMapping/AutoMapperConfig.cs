using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Spaanjaars.ContactManager45.Model;

namespace Spaanjaars.ContactManager45.Web.Wcf
{
public static class AutoMapperConfig
{
  public static void Start()
  {
    Mapper.CreateMap<PersonModel, Person>()
          .ForMember(x => x.Type, x => x.Ignore())
          .ForMember(x => x.DateCreated, x => x.Ignore())
          .ForMember(x => x.DateModified, x => x.Ignore())
          .ForMember(x => x.EmailAddresses, x => x.Ignore())
          .ForMember(x => x.PhoneNumbers, x => x.Ignore())
          .ForMember(x => x.HomeAddress, y => y.Condition(src => !src.IsSourceValueNull))
          .ForMember(x => x.WorkAddress, y => y.Condition(src => !src.IsSourceValueNull));
    Mapper.CreateMap<Person, PersonModel>();

    Mapper.CreateMap<AddressModel, Address>()
          .ForMember(x => x.ContactType, x => x.Ignore());

    Mapper.CreateMap<Address, AddressModel>();

    Mapper.CreateMap<ValidationResult, ValidationResultModel>();

    Mapper.AssertConfigurationIsValid();
  }
}

}