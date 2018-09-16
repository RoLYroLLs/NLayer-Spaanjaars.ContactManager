This sample application is a supplemental download for the articles series "Building N-Layered Applications with ASP.NET 4.5"

You can view the first article here: 

http://imar.spaanjaars.com/573/aspnet-n-layered-applications-introduction-part-1

If you are using Visual Studio 2012 Express Edition you'll see a bunch of *.cd files in 
the class library projects that you can't open. These files contain the class diagrams 
(that are also used in the article) and are not required to run the 
application; it's safe to delete or ignore them.

The download does not contain the full Packages folder due to its size. In order for Visual Studio to download 
the missing packages you need to enable NuGet Package Restore as described here:

http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages

If you can't make this work let me know and I'll provide an alternative download with all packages included in the download.

You'll find one package in the Packages folder (Microsoft.Bcl.Build.1.0.7) as it can't be downloaded by NuGet automatically. For more 
details, see: 

http://connect.microsoft.com/VisualStudio/feedback/details/788981/microsoft-bcl-build-targets-causes-project-loading-to-fail
http://blogs.msdn.com/b/dotnet/archive/2013/06/12/nuget-package-restore-issues.aspx


If you have questions or comments about the articles or code download, 
use the Contact page on my web site or the Talk Back feature at the bottom of the articles on my web site.

Have fun and happy N-Layering,

Imar Spaanjaars
http://imar.spaanjaars.Com
imar@spaanjaars.com

