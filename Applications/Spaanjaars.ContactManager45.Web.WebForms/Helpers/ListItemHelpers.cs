using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Spaanjaars.ContactManager45.Web.WebForms.Helpers
{
  internal static class ListItemHelpers
  {
    internal static IEnumerable<ListItem> EnumToListItems<T>()
    {
      Type type = typeof(T);
      var result = new List<ListItem>();
      var values = Enum.GetValues(type);

      foreach (int value in values)
      {
        string text = Enum.GetName(type, value);
        result.Add(new ListItem(text, value.ToString().PascalCaseToSpaces()));
      }
      return result;
    }

    private static string PascalCaseToSpaces(this string stringValue)
    {
      if (string.IsNullOrEmpty(stringValue))
      {
        return stringValue;
      }
      return Regex.Replace(stringValue, @"((?<=[a-z])[A-Z]\w|(?<=\w)[A-Z][a-z])", @" $0");
    }
  }
}
