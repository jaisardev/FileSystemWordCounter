using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemWordCounter.API.Business.Logging;
using FileSystemWordCounter.API.Business.Attributes;

namespace FileSystemWordCounter.API.Business
{
  [UnityIoCTransientLifetimeAttribute]
  public class UnitOfWork : IUnitOfWork
  {
    static int _counter = 0;
    public UnitOfWork()
    {
      _counter++;
      UnityEventLogger.Log.CreateUnityMessage("UnitOfWorkExampleTest " + _counter);
    }

    public virtual void Deposit(decimal depositAmount)
    {
      string x = "";
    }

    private bool _disposed = false;

    public void Dispose()
    {
      if (!_disposed)
      {
        _counter--;
        UnityEventLogger.Log.DisposeUnityMessage("UnitOfWorkExampleTest " + _counter);
        _disposed = true;
      }
    }

    public string HelloFromUnitOfWorkExample(string value)
    {
      //UnityEventLogger.Log.LogUnityMessage("Hello UnitOfWorkExampleTest " + _counter);
      return value;
    }
  }
}