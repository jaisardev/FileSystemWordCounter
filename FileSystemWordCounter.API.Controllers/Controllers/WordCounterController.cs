using System.Net;
using System.Net.Http;
using System.Web.Http;
using FileSystemWordCounter.API.Business;
using FileSystemWordCounter.API.Business.Model;

namespace FileSystemWordCounter.API.Controllers
{

  public class WordCounterController : ApiController
  {
    private readonly IWordCounter _wordCounter;
    public WordCounterController(IWordCounter wordCounter)
    {
      _wordCounter = wordCounter;
    }

    // GET api/wordCounter 

    public HttpResponseMessage Get()
    {
      return Request.CreateResponse(HttpStatusCode.NotFound);
    }

    // GET api/wordCounter/test 
    [HttpGet("Search/{id}")]
    public virtual HttpResponseMessage Get(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
      
      CounterResultDTO counterResult = new CounterResultDTO();
      counterResult = _wordCounter.GetCounterResults(id);
      if (counterResult == null || counterResult.TotalCoincidencesFound == 0)
      {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }
      return Request.CreateResponse(HttpStatusCode.OK, counterResult);
    }

    // POST api/wordCounter 
    public HttpResponseMessage Post(string id)
    {
      return Request.CreateResponse(HttpStatusCode.NotFound);
    }

    // PUT api/wordCounter/5 
    public HttpResponseMessage Put(int id, [FromBody] string value)
    {
      return Request.CreateResponse(HttpStatusCode.NotFound);
    }

    // DELETE api/wordCounter/5 
    public HttpResponseMessage Delete(int id)
    {
      return Request.CreateResponse(HttpStatusCode.NotFound);
    }
  }
}
