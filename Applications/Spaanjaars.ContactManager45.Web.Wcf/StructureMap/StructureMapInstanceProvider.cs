using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using StructureMap;

namespace Spaanjaars.ContactManager45.Web.Wcf.StructureMap
{
  public class StructureMapInstanceProvider : IInstanceProvider
  {
    private readonly Type _serviceType;

    public StructureMapInstanceProvider(Type serviceType)
    {
      _serviceType = serviceType;
    }

    public object GetInstance(InstanceContext instanceContext, Message message)
    {
      return ObjectFactory.GetInstance(_serviceType);
    }

    public object GetInstance(InstanceContext instanceContext)
    {
      return GetInstance(instanceContext, null);
    }

    public void ReleaseInstance(InstanceContext instanceContext, object instance)
    {
      //No cleanup required
    }
  }

}