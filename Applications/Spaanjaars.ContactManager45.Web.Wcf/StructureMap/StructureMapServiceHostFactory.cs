using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Spaanjaars.ContactManager45.Web.Wcf.StructureMap
{
  public class StructureMapServiceHostFactory : ServiceHostFactory
  {
    public StructureMapServiceHostFactory()
    {
      Ioc.Initialize();
    } 

    protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
    {
      return new StructureMapServiceHost(serviceType, baseAddresses);
    }
  }
}