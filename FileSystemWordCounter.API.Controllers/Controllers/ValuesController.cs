using System.Collections.Generic;
using System.Web.Http;
using FileSystemWordCounter.API.Business;
using FileSystemWordCounter.API.Business.Model;

namespace FileSystemWordCounter.API.Controllers
{
  public class ValuesController : ApiController
  {
    private readonly IWordCounter _wordCounter;
    public ValuesController(IWordCounter wordCounter)
    {
      _wordCounter = wordCounter;
    }

    // GET api/values 
    public IEnumerable<string> Get()
    {
      CounterResultDTO counterResult = new CounterResultDTO();
      counterResult = _wordCounter.GetCounterResults();

      return new string[] { counterResult.TotalFilesFound.ToString(), counterResult.TotalCoincidencesFound.ToString(), counterResult.CoincidencesByFile };
    }

    // GET api/values/5 
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values 
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5 
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5 
    public void Delete(int id)
    {
    }
  }
}
