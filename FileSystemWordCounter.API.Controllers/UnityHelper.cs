using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity;
using System.Linq;
using FileSystemWordCounter.API.Business.Attributes;
using FileSystemWordCounter;

namespace FileSystemWordCounter.API.Controllers
{
  public static class UnityHelpers
  {
    #region Unity Container
    private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
      var container = new UnityContainer();
      RegisterTypes(container);
      return container;
    });

    public static IUnityContainer GetConfiguredContainer()
    {
      return container.Value;
    }
    #endregion

    //private static readonly Type[] EmptyTypes = new Type[0];

    public static IEnumerable<Type> GetTypesWithCustomAttribute<T>(Assembly[] assemblies)
    {
      foreach (var assembly in assemblies)
      {
        foreach (Type type in assembly.GetTypes())
        {
          if (type.GetCustomAttributes(typeof(T), true).Length > 0)
          {
            yield return type;
          }
        }
      }
    }

    public static void RegisterTypes(IUnityContainer container)
    {
      var myAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("FileSystemWordCounter")).ToArray();

      container.RegisterType(typeof(Startup));

      container.RegisterTypes(
          UnityHelpers.GetTypesWithCustomAttribute<UnityIoCContainerControlledAttribute>(myAssemblies),
          WithMappings.FromMatchingInterface,
          WithName.Default,
          WithLifetime.ContainerControlled,
          null
          ).RegisterTypes(
                   UnityHelpers.GetTypesWithCustomAttribute<UnityIoCTransientLifetimeAttribute>(myAssemblies),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.Transient);

    }

  }
}
