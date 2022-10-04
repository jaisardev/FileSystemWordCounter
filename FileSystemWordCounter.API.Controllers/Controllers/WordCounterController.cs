using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
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
    [HttpGet("Search/{text}")]
    public virtual HttpResponseMessage Get(string folder, string text)
    {
      folder = Decode(folder);
      if (string.IsNullOrWhiteSpace(text))
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
      
      CounterResultDTO counterResult = new CounterResultDTO();
      counterResult = _wordCounter.GetCounterResults(folder, text);
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
    private static string Decode(string content)
    {
      var decodedBytes = HttpServerUtility.UrlTokenDecode(content);
      string externalId = UTF8Encoding.UTF8.GetString(decodedBytes);

      return externalId;
    }
  }
}
