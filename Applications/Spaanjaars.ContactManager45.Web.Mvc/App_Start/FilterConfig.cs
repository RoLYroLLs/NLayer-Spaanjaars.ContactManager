using System.Web.Mvc;

namespace Spaanjaars.ContactManager45.Web.Mvc.App_Start
{
  public static class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}