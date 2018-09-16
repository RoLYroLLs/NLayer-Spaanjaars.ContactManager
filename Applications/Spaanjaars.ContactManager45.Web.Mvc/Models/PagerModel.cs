using System.Collections.Generic;

namespace Spaanjaars.ContactManager45.Web.Mvc.Models
{
  public class PagerModel<T> where T : class
  {
    public IEnumerable<T> Data { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalRows { get; set; }
  }
}