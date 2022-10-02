using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemWordCounter.API.Business.Logging;
using FileSystemWordCounter.API.Business.Attributes;
using FileSystemWordCounter.API.Business.Model;

namespace FileSystemWordCounter.API.Business
{
  [UnityIoCTransientLifetimeAttribute]
  public class WordCounter : IWordCounter
  {
    private IUnitOfWork _unitOfWorkExample;
    private string _searchTerm = "error";
    private bool _disposed = false;
    private CounterResultDTO _counterResult;

    public WordCounter(IUnitOfWork unitOfWorkExample)
    {
      _unitOfWorkExample = unitOfWorkExample;
      UnityEventLogger.Log.CreateUnityMessage("BusinessClass");
    }

    //public string Hello()
    //{
    //  return _unitOfWorkExample.HelloFromUnitOfWorkExample();
    //}

    public CounterResultDTO GetCounterResults()
    {
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
        coincidencesByFile += "\r\n" + filename + " (" + fileCoincidences.ToString() + ")";
        resultDTO.CoincidencesByFile = filename + " (" + fileCoincidences.ToString() + ")";
        totalFilesCoincidences = totalFilesCoincidences + fileCoincidences;
      }

      resultDTO.TotalFilesFound = queryMatchingFiles.ToList().Count();
      resultDTO.CoincidencesByFile = coincidencesByFile;
      resultDTO.TotalCoincidencesFound = totalFilesCoincidences;

      return resultDTO;
    }

    public void Dispose()
    {
      _unitOfWorkExample.Dispose();
      UnityEventLogger.Log.DisposeUnityMessage("BusinessClass");
      if (!_disposed)
      {
        _disposed = true;
      }
    }

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
  }
}