using AutoMapper;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Web.Mvc.Models;

namespace Spaanjaars.ContactManager45.Web.Mvc.App_Start
{
  public static class AutoMapperConfig
  {
    public static void Start()
    {
      #region Person

      Mapper.CreateMap<Person, DisplayPerson>();

      Mapper.CreateMap<CreateAndEditPerson, Person>()
            .ForMember(d => d.DateCreated, t => t.Ignore())
            .ForMember(d => d.DateModified, t => t.Ignore())
            .ForMember(d => d.EmailAddresses, t => t.Ignore())
            .ForMember(d => d.PhoneNumbers, t => t.Ignore())
            .ForMember(d => d.HomeAddress, t => t.Ignore())
            .ForMember(d => d.WorkAddress, t => t.Ignore());
      Mapper.CreateMap<Person, CreateAndEditPerson>();
      #endregion

      #region Address

      Mapper.CreateMap<Address, EditAddress>()
            .ForMember(d => d.PersonId, t => t.Ignore());
      Mapper.CreateMap<EditAddress, Address>().ConstructUsing(s => new Address(s.Street, s.City, s.ZipCode, s.Country, s.ContactType));
      #endregion

      #region E-mail addresses

      Mapper.CreateMap<EmailAddress, DisplayEmailAddress>()
            .ForMember(d => d.PersonId, t => t.MapFrom(y => y.OwnerId));
      Mapper.CreateMap<EmailAddress, CreateAndEditEmailAddress>()
            .ForMember(d => d.PersonId, t => t.MapFrom(y => y.OwnerId));
      Mapper.CreateMap<CreateAndEditEmailAddress, EmailAddress>()
            .ForMember(d => d.OwnerId, t => t.Ignore())
            .ForMember(d => d.Owner, t => t.Ignore());

      #endregion

      #region Phone number

      Mapper.CreateMap<PhoneNumber, DisplayPhoneNumber>();
      Mapper.CreateMap<PhoneNumber, CreateAndEditPhoneNumber>();
      Mapper.CreateMap<CreateAndEditPhoneNumber, PhoneNumber>()
            .ForMember(d => d.OwnerId, t => t.Ignore())
            .ForMember(d => d.Owner, t => t.Ignore());

      #endregion

      Mapper.AssertConfigurationIsValid();
    }
  }
}