using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWordCounter.API.Business
{
  public interface IUnitOfWork : IDisposable
  {
    string HelloFromUnitOfWorkExample(string value);

    void Deposit(decimal depositAmount);
  }
}
