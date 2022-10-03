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

    public int _totalFilesFound;
    public int _totalCoincidencesFound;
    public List<string> _coincidencesByFile = new List<string>();

    public int TotalFilesFound 
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

    public int TotalCoincidencesFound
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
        return _coincidencesByFile;
      }
      set
      {
        _coincidencesByFile = value;
        OnPropertyChanged("CoincidencesByFile");
      }
    }

    public HttpStatusCode StatusCode { get; internal set; }
  }
}
