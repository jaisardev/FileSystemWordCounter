using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWordCounter.API.Business.Model
{
  public class CounterResultDTO
  {
    public int TotalFilesFound;
    public int TotalCoincidencesFound;
    public List<string> CoincidencesByFile = new List<string>();
  }
}
