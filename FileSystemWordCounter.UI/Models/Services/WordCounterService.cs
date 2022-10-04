using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWordCounter.UI.Models.Services
{
  public static class WordCounterService
  {
    public static readonly String BASE_URL = "http://localhost:8081/api/WordCounter";


    //public static async Task<WordCounterResult> GetResults(string query)
    //{
    //  WordCounterResult result = new WordCounterResult();

    //  string url = BASE_URL + "/Get/" + query;

    //  using (HttpClient client = new HttpClient())
    //  {
    //    var response = await client.GetAsync(url);
    //    string json = await response.Content.ReadAsStringAsync();

    //    result = JsonConvert.DeserializeObject<WordCounterResult>(json);
    //  }

    //  return result;
    //}

    public static async Task<WordCounterResult> GetResults(String folderToSearch, String textToSearch)
    {
      try
      {
        WordCounterResult result = new WordCounterResult();
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        string apiUrl = $"{BASE_URL}/Get/" + folderToSearch + "/" + textToSearch;
        using (HttpClient client = new HttpClient())
        {
          client.BaseAddress = new Uri(apiUrl);
          client.Timeout = TimeSpan.FromSeconds(900);
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          var response = client.GetAsync(apiUrl);
          response.Wait();
          string json = await response.Result.Content.ReadAsStringAsync();

          result = JsonConvert.DeserializeObject<WordCounterResult>(json);
          return result;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
