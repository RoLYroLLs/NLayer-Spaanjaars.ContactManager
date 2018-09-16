using System.ServiceModel;

namespace Spaanjaars.ContactManager45.Web.Wcf
{
  [ServiceContract]
  public interface IContactManagerService
  {
    [OperationContract]
    PersonModel GetPerson(int id);

    [OperationContract]
    ServiceResult<int?> InsertPerson(PersonModel personModel);

    [OperationContract]
    ServiceResult<int?> UpdatePerson(PersonModel personModel);

    [OperationContract]
    void DeletePerson(int id);
  }
}
