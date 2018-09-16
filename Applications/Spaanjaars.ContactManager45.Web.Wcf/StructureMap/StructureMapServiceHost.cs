using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Spaanjaars.ContactManager45.Web.Wcf.StructureMap
{
  public class StructureMapServiceHost : ServiceHost
  {
    public StructureMapServiceHost()
    {
    }

    public StructureMapServiceHost(Type serviceType, params Uri[] baseAddresses)
      : base(serviceType, baseAddresses)
    {
    }

    protected override void OnOpening()
    {
      Description.Behaviors.Add(new StructureMapServiceBehavior());
      base.OnOpening();
    }
  }
}
