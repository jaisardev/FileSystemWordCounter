using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FileSystemWordCounter.API.Business.Model;

namespace FileSystemWordCounter.API.Business
{
  public interface IWordCounter : IDisposable
  {
    CounterResultDTO GetCounterResults(string searchTerm);
  }
}