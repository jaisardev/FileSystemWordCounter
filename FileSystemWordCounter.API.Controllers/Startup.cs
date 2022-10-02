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

    public static void StartServer()
    {
      string baseAddress = "http://localhost:8081/";

      // Start OWIN host 
      using (WebApp.Start<Startup>(url: baseAddress))
      {
        // Create HttpClient and make a request to api/values 
        HttpClient client = new HttpClient();

        var response = client.GetAsync(baseAddress + "api/values").Result;

        Console.WriteLine("Started...");
        Console.WriteLine(response);
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        Console.ReadLine();
      }

      try
      {
        Console.ReadKey();
      }
      finally
      {
      }


    }
    // This code configures Web API. The Startup class is specified as a type
    public void Configuration(IAppBuilder appBuilder)
    {
      // Configure Web API for self-host. 
      HttpConfiguration config = new HttpConfiguration();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      appBuilder.UseWebApi(config);
    }
  }
}
