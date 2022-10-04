#region using namespaces.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using FileSystemWordCounter.API.Business.Model;
using FileSystemWordCounter.API.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Http.Hosting;
using Newtonsoft.Json;

#endregion

namespace FileSystemWordCounter.API.Business.Test
{
  /// <summary>
  /// WordCounter Test
  /// </summary>
  public class WordCounterControllerTest
  {
    #region Variables

    private IUnitOfWork _unitOfWork;
    private IWordCounter _wordCounter;
    private const string _serviceBaseURL = "http://localhost:8081/";
    private HttpResponseMessage _response;

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

      var mockWordCounterController = new Mock<WordCounterController>(MockBehavior.Default);
      HttpRequestMessage request = new HttpRequestMessage();
      mockWordCounterController.Setup(c => c.Get(It.IsAny<string>(), It.Is<string>(x => x == "NotFound"))).Returns(request.CreateResponse(HttpStatusCode.NotFound));

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
      mockWordCounter.Setup(a => a.GetCounterResults(It.IsAny<string>(), null)).Returns(new CounterResultDTO());
      mockWordCounter.Setup(a => a.GetCounterResults(It.IsAny<string>(), It.Is<string>(x => x == ""))).Returns(new CounterResultDTO());
      mockWordCounter.Setup(a => a.GetCounterResults(It.IsAny<string>(), It.Is<string>(x => x == "test"))).Returns(counterResultDTO);

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
      var wordCounterController = new WordCounterController(_wordCounter)
      {
        Request = new HttpRequestMessage
        {
          Method = HttpMethod.Get,
          RequestUri = new Uri(_serviceBaseURL + "WordCounter/Get/test")
        }
      };

      wordCounterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

      _response = wordCounterController.Get(@"c:\temp", "test");

      var responseResult = JsonConvert.DeserializeObject<CounterResultDTO>(_response.Content.ReadAsStringAsync().Result);
      Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);

      // Object
      Assert.IsInstanceOf<CounterResultDTO>(responseResult);
      Assert.IsNotNull(responseResult);

      // property CoincidencesByFile
      Assert.IsNotEmpty(responseResult.CoincidencesByFile);
      Assert.Greater(responseResult.CoincidencesByFile.Count(), 0);

      // Property TotalCoincidencesFound
      Assert.Greater(responseResult.TotalCoincidencesFound, 0);

      // Property TotalFilesFound
      Assert.Greater(responseResult.TotalFilesFound, 0);
    }

    /// <summary>
    /// Get the search results
    /// </summary>
    [Test]
    public void GetCounterResultsByEmptySearchParameter()
    {
      var wordCounterController = new WordCounterController(_wordCounter)
      {
        Request = new HttpRequestMessage
        {
          Method = HttpMethod.Get,
          RequestUri = new Uri(_serviceBaseURL + "WordCounter/Get/test/")
        }
      };

      wordCounterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

      _response = wordCounterController.Get(@"c:\temp", "");

      Assert.AreEqual(_response.StatusCode, HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Get the search results
    /// </summary>
    [Test]
    public void GetCounterResultsByNullSearchParameter()
    {
      var wordCounterController = new WordCounterController(_wordCounter)
      {
        Request = new HttpRequestMessage
        {
          Method = HttpMethod.Get,
          RequestUri = new Uri(_serviceBaseURL + "WordCounter/Get/test/")
        }
      };

      wordCounterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

      _response = wordCounterController.Get(@"c:\temp", null);

      Assert.AreEqual(_response.StatusCode, HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Get the search results
    /// </summary>
    [Test]
    public void GetCounterResultsBySearchNotFoundParameter()
    {
      var wordCounterController = new WordCounterController(_wordCounter)
      {
        Request = new HttpRequestMessage
        {
          Method = HttpMethod.Get,
          RequestUri = new Uri(_serviceBaseURL + "WordCounter/Get/test/NotFound")
        }
      };

      wordCounterController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

      _response = wordCounterController.Get(@"c:\temp", "NotFound");

      Assert.AreEqual(_response.StatusCode, HttpStatusCode.NotFound);
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
      _response = null;
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
      _response = null;
    }

    #endregion
  }
}

