using import.Interfaces;
using import.Loaders;
using Microsoft.Extensions.Logging;
using Moq;
using System.Xml.Linq;
using YamlDotNet.Core;

namespace import.Tests.UnitTests.Loaders
{
    public class CapterraLoaderTests
    {
        /*********************
         * Process Method Tests
         *********************/

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Process_MissingName_ThrowsArgumentNullException(string? name)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var fileContents = GetYamlData();
            ICapterraLoader loader = new CapterraLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            loader.Process( fileContents, name, "description"));

            Assert.Equal("name", result.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Process_EmptyContents_ThrowsArgumentNullException(string? fileContents)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            ICapterraLoader loader = new CapterraLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert 
            var result = Assert.Throws<ArgumentNullException>(() =>
            loader.Process(fileContents, "name", "description"));

            Assert.Equal("contents", result.ParamName);
        }

        /*********************
         * Process Tests
         *********************/

        [Theory]
        [InlineData("{\r\n    \"products\": [\r\n        {\r\n            \"categories\": [\r\n                \"Customer Service\",\r\n                \"Call Center\"\r\n            ],\r\n            \"twitter\": \"@freshdesk\",\r\n            \"title\": \"Freshdesk\"\r\n        },\r\n        {\r\n            \"categories\": [\r\n                \"CRM\",\r\n                \"Sales Management\"\r\n            ],\r\n            \"title\": \"Zoho\"\r\n        }\r\n    ]\r\n}\r\n")]
        public void Process_InvalidYaml_ThrowsYamlException(string fileContents)
        {
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var loader = new CapterraLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert 
            var result = Assert.Throws<YamlException>(() => loader.Process(fileContents, "name", "description"));
        }
        [Theory]
        [InlineData(@"-")]
        [InlineData(@"---")]
        public void Process_InvalidYaml_ThrowsNullReferenceException(string fileContents)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var loader = new CapterraLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert 
            var result = Assert.Throws<NullReferenceException>(() => loader.Process(fileContents, "name", "description"));
        }

        [Fact]
        public void Process_ValidYaml_ConvertsCorrectNumberOfItems()
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var fileContents = GetYamlData();
            var loader = new CapterraLoader(stubRepo.Object, stubLogger.Object);

            // Act            
            loader.Process(fileContents, "name", "description");

            // Assert 
            Assert.NotNull(loader.Loaded);
            Assert.NotNull(loader.Converted);
            Assert.Equal(3, loader.Converted.Items.Count());
        }

        [Fact]
        public void Process_ValidYaml_SaveTosRepository()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.SaveAsync(It.IsNotNull<IInventory>()));                        
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var fileContents = GetYamlData();
            var loader = new CapterraLoader(mockRepo.Object, stubLogger.Object);

            // Act
            loader.Process(fileContents, "name", "description");

            // Assert 

            mockRepo.Verify(repo => repo.SaveAsync(loader.Converted), Times.Once);
        }


        /*********************
         * TestData
         *********************/
        string GetYamlData()
        {
            return
                "---\r\n-\r\n  tags: \"Bugs & Issue Tracking,Development Tools\"\r\n  name: \"GitGHub\"\r\n  twitter: \"github\"\r\n-\r\n  tags: \"Instant Messaging & Chat,Web Collaboration,Productivity\"\r\n  name: \"Slack\"\r\n  twitter: \"slackhq\"\r\n-\r\n  tags: \"Project Management,Project Collaboration,Development Tools\"\r\n  name: \"JIRA Software\"\r\n  twitter: \"jira\"\r\n";
        }

        string GetJsonData()
        {
            return "{\r\n    \"products\": [\r\n        {\r\n            \"categories\": [\r\n                \"Customer Service\",\r\n                \"Call Center\"\r\n            ],\r\n            \"twitter\": \"@freshdesk\",\r\n            \"title\": \"Freshdesk\"\r\n        },\r\n        {\r\n            \"categories\": [\r\n                \"CRM\",\r\n                \"Sales Management\"\r\n            ],\r\n            \"title\": \"Zoho\"\r\n        }\r\n    ]\r\n}\r\n";
        }


    }
}