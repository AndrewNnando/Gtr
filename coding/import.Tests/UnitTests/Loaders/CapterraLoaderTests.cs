using import.Interfaces;
using import.Loaders;
using Microsoft.Extensions.Logging;
using Moq;
using System.Xml.Linq;

namespace import.Tests.UnitTests.Loaders
{
    public class CapterraLoaderTests
    {
        /*********************
         * Constructor Tests
         *********************/

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_MissingName_ThrowsArgumentNullException(string? name)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var fileContents = GetYamlData();
            // Act and Assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            new CapterraLoader(stubRepo.Object, stubLogger.Object, fileContents, name, "description"));

            Assert.Equal("name", result.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_EmptyContents_ThrowsArgumentNullException(string? fileContents)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();

            // Act and Assert 
            var result = Assert.Throws<ArgumentNullException>(() =>
            new CapterraLoader(stubRepo.Object, stubLogger.Object, fileContents, "name", "description"));

            Assert.Equal("contents", result.ParamName);
        }

        /*********************
         * Process Tests
         *********************/

        [Theory]
        [InlineData(@"---")]
        public void Process_InvalidYaml_ThrowsException(string fileContents)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();

            // Act
            var loader = new CapterraLoader(stubRepo.Object, stubLogger.Object, fileContents, "name", "description");

            // Assert 
            var result = Assert.Throws<Exception>(() => loader.Process());

            Assert.Equal("The file contents could not be deserialized.", result.Message);
            Assert.Null(loader.Loaded);
        }
        [Theory]
        [InlineData(@"-")]
        public void Process_InvalidYaml_ThrowsNullReferenceException(string fileContents)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();

            // Act
            var loader = new CapterraLoader(stubRepo.Object, stubLogger.Object, fileContents, "name", "description");

            // Assert 
            var result = Assert.Throws<NullReferenceException>(() => loader.Process());
        }


        [Fact]
        public void Process_ValidYaml_ConvertsCorrectNumberOfItems()
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<CapterraLoader>>();
            var fileContents = GetYamlData();

            // Act
            var loader = new CapterraLoader(stubRepo.Object, stubLogger.Object, fileContents, "name", "description");
            loader.Process();

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

            // Act
            var loader = new CapterraLoader(mockRepo.Object, stubLogger.Object, fileContents, "name", "description");
            loader.Process();

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

    }
}