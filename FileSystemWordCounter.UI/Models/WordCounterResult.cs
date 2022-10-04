using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWordCounter.UI.Models
{
  public class WordCounterResult : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    private string _totalFilesFound;
    private string _totalCoincidencesFound;
    private List<string> _coincidencesByFile = new List<string>();
    private string _coincidencesByFileString;
    private string _folder;
    private string _text;

    public string Folder
    {
      get
      {
        return _folder;
      }
      set
      {
        _folder = value;
        OnPropertyChanged("Folder");
      }
    }

    public string Text
    {
      get
      {
        return _text;
      }
      set
      {
        _text = value;
        OnPropertyChanged("Text");
      }
    }


    public string TotalFilesFound 
    { 
      get 
        { 
          return _totalFilesFound; 
        } 
      set 
        {
          _totalFilesFound = value; 
          OnPropertyChanged("TotalFilesFound"); 
        } 
    }

    public string TotalCoincidencesFound
    {
      get
      {
        return _totalCoincidencesFound;
      }
      set
      {
        _totalCoincidencesFound = value;
        OnPropertyChanged("TotalCoincidencesFound");
      }
    }

    public List<string> CoincidencesByFile
    {
      get
      {
        return  _coincidencesByFile;
      }
      set
      {
        _coincidencesByFile = value;
        OnPropertyChanged("CoincidencesByFile");
      }
    }

    public string CoincidencesByFileString
    {
      get
      {
        return _coincidencesByFileString;
      }
      set
      {
        _coincidencesByFileString = value;
        OnPropertyChanged("CoincidencesByFileString");
      }
    }

    public HttpStatusCode StatusCode { get; internal set; }
  }
}
