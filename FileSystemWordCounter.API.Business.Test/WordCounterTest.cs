#region using namespaces.
using System;
using System.Collections.Generic;
using System.Linq;
using FileSystemWordCounter.API.Business.Model;
using Moq;
using NUnit.Framework;

#endregion

namespace FileSystemWordCounter.API.Business.Test
{
  /// <summary>
  /// WordCounter Test
  /// </summary>
  public class WordCounterTest
  {
    #region Variables

    private IWordCounter _wordCounter;
    private IUnitOfWork _unitOfWork;

    #endregion

    #region Test fixture setup

    /// <summary>
    /// Initial setup for tests
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {

    }

    #endregion

    #region Setup

    /// <summary>
    /// Re-initializes test.
    /// </summary>
    [SetUp]
    public void ReInitializeTest()
    {
      var unitOfWork = new Mock<IUnitOfWork>();
      _unitOfWork = unitOfWork.Object;

      var wordCounter = SetUpWordCounter();
      _wordCounter = wordCounter.Object;

    }

    #endregion

    #region Private member methods

    /// <summary>
    /// Setup WordCounter
    /// </summary>
    /// <returns></returns>
    private Mock<WordCounter> SetUpWordCounter()
    {
      // Initialise WordCounter
      var mockWordCounter = new Mock<WordCounter>(MockBehavior.Default);

      mockWordCounter.SetupAllProperties();

      // Setup mocking behavior
      List<string> filesfound = new List<string>();
      filesfound.Add("test.txt (1)");
      filesfound.Add("test2.txt (2)");

      var counterResultDTO = new CounterResultDTO();

      counterResultDTO.CoincidencesByFile = filesfound;
      counterResultDTO.TotalFilesFound = 2;
      counterResultDTO.TotalCoincidencesFound = 3;

      // GetCounterResults could have any string and specific or empty results
      mockWordCounter.Setup(a => a.GetCounterResults(null)).Returns(new CounterResultDTO());
      mockWordCounter.Setup(a => a.GetCounterResults(It.Is<string>(x => x == ""))).Returns(new CounterResultDTO());
      mockWordCounter.Setup(a => a.GetCounterResults(It.Is<string>(x => x == "test"))).Returns(counterResultDTO);


      // Return mock implementation object
      return mockWordCounter;
    }

    #endregion

    #region Unit Tests

    /// <summary>
    /// Get the search results Ok
    /// </summary>
    [Test]
    public void GetCounterResultsByRightSearchParameter()
    {
      var testwordCounter = _wordCounter.GetCounterResults("test");

      // Obejct
      Assert.IsInstanceOf<CounterResultDTO>(testwordCounter);
      Assert.IsNotNull(testwordCounter);

      // property CoincidencesByFile
      Assert.IsNotEmpty(testwordCounter.CoincidencesByFile);
      Assert.Greater(testwordCounter.CoincidencesByFile.Count(), 0);

      // Property TotalCoincidencesFound
      Assert.Greater(testwordCounter.TotalCoincidencesFound, 0);

      // Property TotalFilesFound
      Assert.Greater(testwordCounter.TotalFilesFound, 0);
    }

    /// <summary>
    /// Get the search results
    /// </summary>
    [Test]
    public void GetCounterResultsByEmptySearchParameter()
    {
      var testwordCounter = _wordCounter.GetCounterResults("");

      // Obejct
      Assert.IsInstanceOf<CounterResultDTO>(testwordCounter);
      Assert.IsNotNull(testwordCounter);

      // Property CoincidencesByFile
      Assert.IsEmpty(testwordCounter.CoincidencesByFile);

      // Property TotalCoincidencesFound
      Assert.AreEqual(testwordCounter.TotalCoincidencesFound, 0);

      // Property TotalFilesFound
      Assert.AreEqual(testwordCounter.TotalFilesFound, 0);
    }

    /// <summary>
    /// Get the search results
    /// </summary>
    [Test]
    public void GetCounterResultsByNullSearchParameter()
    {
      var testwordCounter = _wordCounter.GetCounterResults(null);

      // Object
      Assert.IsInstanceOf<CounterResultDTO>(testwordCounter);
      Assert.IsNotNull(testwordCounter);

      // Property CoincidencesByFile
      Assert.IsEmpty(testwordCounter.CoincidencesByFile);

      // Property TotalCoincidencesFound
      Assert.AreEqual(testwordCounter.TotalCoincidencesFound, 0);

      // Property TotalFilesFound
      Assert.AreEqual(testwordCounter.TotalFilesFound, 0);
    }


    #endregion

    #region Tear Down

    /// <summary>
    /// Tears down each test data
    /// </summary>
    [TearDown]
    public void DisposeTest()
    {
      _unitOfWork = null;
      _wordCounter = null;
    }

    #endregion

    #region TestFixture TearDown.

    /// <summary>
    /// TestFixture teardown
    /// </summary>
    [OneTimeTearDown]
    public void DisposeAllObjects()
    {
      _unitOfWork = null;
      _wordCounter = null;
    }

    #endregion
  }
}
