using Owin;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using Microsoft.Practices.Unity;

namespace FileSystemWordCounter.API.Controllers
{
  public class Startup
  {
    private static readonly IUnityContainer _container = UnityHelpers.GetConfiguredContainer();

    public static void StartServer()
    {
      string baseAddress = "http://localhost:8081/";
      var startup = _container.Resolve<Startup>();
      IDisposable webApplication = WebApp.Start(baseAddress, startup.Configuration);

      try
      {
        Console.WriteLine("Started...");

        Console.ReadKey();
      }
      finally
      {
        webApplication.Dispose();
      }


    }
    // This code configures Web API. The Startup class is specified as a type
    public void Configuration(IAppBuilder appBuilder)
    {
      // Configure Web API for self-host. 
      HttpConfiguration config = new HttpConfiguration();
      config.DependencyResolver = new UnityDependencyResolver(UnityHelpers.GetConfiguredContainer());

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{action=Index}/{folder}/{text}", 
      defaults: new { id = RouteParameter.Optional }
      );
      config.MapHttpAttributeRoutes();

      appBuilder.UseWebApi(config);
    }
  }
}
