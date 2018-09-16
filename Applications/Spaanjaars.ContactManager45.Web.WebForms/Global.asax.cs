using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Spaanjaars.ContactManager45.Web.WebForms
{
  public class Global : HttpApplication
  {
    void Application_Start(object sender, EventArgs e)
    {
      // Code that runs on application startup
      // Use LocalDB for Entity Framework by default
      Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\\v11.0; Integrated Security=True; MultipleActiveResultSets=True");
    }

    void Application_End(object sender, EventArgs e)
    {
      //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
      // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e)
    {
      // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e)
    {
      // Code that runs when a session ends.
      // Note: The Session_End event is raised only when the sessionstate mode
      // is set to InProc in the Web.config file. If session mode is set to StateServer
      // or SQLServer, the event is not raised.
    }
  }
}
