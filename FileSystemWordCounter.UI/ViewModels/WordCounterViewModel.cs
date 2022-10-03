using FileSystemWordCounter.UI.Models;
using FileSystemWordCounter.UI.Models.Services;

namespace FileSystemWordCounter.UI.ViewModels
{
  public class WordCounterViewModel
  {
    private WordCounterResult _wordCounterResult;
    //private WordCounterService _wordCounterService;

    public WordCounterViewModel()
    {
      //_wordCounterService = new WordCounterService();
      _wordCounterResult = new WordCounterResult { CoincidencesByFile = new System.Collections.Generic.List<string>() { "1.txt (2)", "2.txt(1)" }, TotalFilesFound = 3, TotalCoincidencesFound = 2 };
    }

    public WordCounterResult WordCounterResult
    {
      get { return _wordCounterResult; }
      set { _wordCounterResult = value; }
    }

    private void GetResults()
    {
      //var wordCounterService = new WordCounterService();
      //var results = wordCounterService.GetResults();

      //if (results.Result.StatusCode == System.Net.HttpStatusCode.OK)
      //{
      //  WordCounterResult = results.Result;
      //}

      //WordCounterService.GetResults(employeeModel);
    }
  }
}
