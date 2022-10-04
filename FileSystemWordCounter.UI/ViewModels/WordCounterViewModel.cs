using FileSystemWordCounter.UI.Command;
using FileSystemWordCounter.UI.Models;
using FileSystemWordCounter.UI.Models.Services;
using System.Windows.Input;
using System;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;

namespace FileSystemWordCounter.UI.ViewModels
{
  public class WordCounterViewModel : INotifyPropertyChanged
  {
    #region PropertyChangeEvent
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChange(string propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion
    private WordCounterResult _wordCounterResult;
    private ICommand _ButtonSearch;
    public ICommand ButtonSearch
    {
      get
      {
        return _ButtonSearch;
      }
      set
      {
        _ButtonSearch = value;
      }
    }

    public WordCounterViewModel()
    {
      _wordCounterResult = new WordCounterResult();
      ButtonSearch = new RelayCommand(new Action<object>(GetResults));
    }

    public WordCounterResult WordCounterResult
    {
      get { return _wordCounterResult; }
      set { _wordCounterResult = value; }
    }

    public string TotalFilesFound
    {
      get { return _wordCounterResult.TotalFilesFound; }
      set { _wordCounterResult.TotalFilesFound = value; OnPropertyChange("TotalFilesFound"); }
    }

    public string TotalCoincidencesFound
    {
      get { return _wordCounterResult.TotalCoincidencesFound; }
      set { _wordCounterResult.TotalCoincidencesFound = value; OnPropertyChange("TotalCoincidencesFound"); }
    }
    public List<String> CoincidencesByFile
    {
      get { return _wordCounterResult.CoincidencesByFile; }
      set { _wordCounterResult.CoincidencesByFile = _wordCounterResult.CoincidencesByFile; OnPropertyChange("CoincidencesByFile"); }
    }

    public void GetResults(object obj)
    {
      var results = WordCounterService.GetResults(Encode(_wordCounterResult.Folder), _wordCounterResult.Text);

      if (results.Result != null)
      {
        WordCounterResult.TotalFilesFound = results.Result.TotalFilesFound.ToString();
        WordCounterResult.TotalCoincidencesFound = results.Result.TotalCoincidencesFound.ToString();
        WordCounterResult.CoincidencesByFileString = String.Join(Environment.NewLine, results.Result.CoincidencesByFile);
      }
      else
      {
        WordCounterResult.TotalFilesFound = "0";
        WordCounterResult.TotalCoincidencesFound = "0";
        WordCounterResult.CoincidencesByFileString = "Not Found";
      }
    }
    private static string Encode(string content)
    {
      byte[] encodedBytes = UTF8Encoding.UTF8.GetBytes(content);
      string externalIdEncoded = HttpServerUtility.UrlTokenEncode(encodedBytes);

      return externalIdEncoded;
    }
  }
}
