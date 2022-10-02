using System;
using System.Collections.Generic;
using System.Linq;
using FileSystemWordCounter.API.Business.Logging;
using FileSystemWordCounter.API.Business.Attributes;
using FileSystemWordCounter.API.Business.Model;

namespace FileSystemWordCounter.API.Business
{
  [UnityIoCTransientLifetimeAttribute]
  public class WordCounter : IWordCounter
  {
    #region "Private variables"
    private IUnitOfWork _unitOfWork;
    private string _searchTerm = string.Empty;
    private bool _disposed = false;
    #endregion

    #region "Constructor"

    public WordCounter()
    {
      UnityEventLogger.Log.CreateUnityMessage("WordCounter");
    }

    public WordCounter(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      UnityEventLogger.Log.CreateUnityMessage("WordCounter");
    }
    #endregion

    #region "Public methods"

    //public string Hello()
    //{
    //  return _unitOfWork.HelloFromUnitOfWorkExample();
    //}
    public virtual CounterResultDTO GetCounterResults(string searchTerm)
    {
      _searchTerm = searchTerm;
      string coincidencesByFile = string.Empty;
      CounterResultDTO resultDTO = new CounterResultDTO();

      // Modify this path as necessary.  
      string startFolder = @"c:\temp";

      // Take a snapshot of the file system.  
      System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

      // This method assumes that the application has discovery permissions  
      // for all folders under the specified path.  
      IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

      // Search the contents of each file.  
      // A regular expression created with the RegEx class  
      // could be used instead of the Contains method.  
      // queryMatchingFiles is an IEnumerable<string>.  
      var queryMatchingFiles =
          from file in fileList
          where file.Extension == ".txt"
          let fileText = GetFileText(file.FullName)
          where fileText.Contains(_searchTerm)
          select file.FullName;

      int totalFilesCoincidences = 0;
      int fileCoincidences = 0;
      foreach (string filename in queryMatchingFiles)
      {
        fileCoincidences = GetCoincidences(filename);
        coincidencesByFile += filename + " (" + fileCoincidences.ToString() + ")";
        resultDTO.CoincidencesByFile.Add(filename + " (" + fileCoincidences.ToString() + ")");
        totalFilesCoincidences = totalFilesCoincidences + fileCoincidences;
      }

      resultDTO.TotalFilesFound = queryMatchingFiles.ToList().Count();
      resultDTO.TotalCoincidencesFound = totalFilesCoincidences;

      return resultDTO;
    }

    public void Dispose()
    {
      _unitOfWork.Dispose();
      UnityEventLogger.Log.DisposeUnityMessage("BusinessClass");
      if (!_disposed)
      {
        _disposed = true;
      }
    }

    #endregion

    #region "Private methods"
    // Read the contents of the file.  
    private string GetFileText(string name)
    {
      string fileContents = String.Empty;

      // If the file has been deleted since we took
      // the snapshot, ignore it and return the empty string.  
      if (System.IO.File.Exists(name))
      {
        fileContents = System.IO.File.ReadAllText(name);
      }
      return fileContents;
    }

    private int GetCoincidences(string name)
    {
      string fileContents = String.Empty;

      // If the file has been deleted since we took
      // the snapshot, ignore it and return the empty string.  
      if (System.IO.File.Exists(name))
      {
        fileContents = System.IO.File.ReadAllText(name);
      }

      //Convert the string into an array of words  
      string[] source = fileContents.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

      // Create the query.  Use the InvariantCultureIgnoreCase comparision to match "data" and "Data"
      var matchQuery = from word in source
                       where word.Equals(_searchTerm, StringComparison.InvariantCultureIgnoreCase)
                       select word;

      // Count the matches, which executes the query.  
      int wordCount = matchQuery.Count();
      //Console.WriteLine("{0} occurrences(s) of the search term \"{1}\" were found.", wordCount, _searchTerm);

      //Keep console window open in debug mode
      //Console.WriteLine("Press any key to exit");
      //Console.ReadKey();

      return wordCount;
    }

    #endregion
  }
}