using import.Interfaces;
using import.Loaders;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using System.Xml.Linq;

namespace import.Tests.UnitTests.Loaders
{

    public class SoftwareAdviceLoaderTests
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
            var stubLogger = new Mock<ILogger<SoftwareAdviceLoader>>();
            var fileContents = GetProductList();
            ISoftwareAdviceLoader loader = new SoftwareAdviceLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            loader.Process(fileContents, name, "description"));

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
            var stubLogger = new Mock<ILogger<SoftwareAdviceLoader>>();
            ISoftwareAdviceLoader loader = new SoftwareAdviceLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert 
            var result = Assert.Throws<ArgumentNullException>(() =>
            loader.Process(fileContents, "name", "description"));

            Assert.Equal("contents", result.ParamName);
        }

        [Theory]
        [InlineData("{")]
        [InlineData("123456")]
        [InlineData("\"categories\": [\"Customer Service\",\"Call Center\"]")]
        [InlineData(@"-")]
        public void Process_InvalidJson_ThrowsJsonException(string fileContents)
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<SoftwareAdviceLoader>>();
            var loader = new SoftwareAdviceLoader(stubRepo.Object, stubLogger.Object);

            // Act and Assert 
             Assert.Throws<JsonException>(() => loader.Process(fileContents, "name", "description"));            
        }


        [Fact]
        public void Process_ValidJson_ConvertsCorrectNumberOfItems()
        {
            // Arrange
            var stubRepo = new Mock<IRepository>();
            var stubLogger = new Mock<ILogger<SoftwareAdviceLoader>>();
            var fileContents = GetProductList();
            var loader = new SoftwareAdviceLoader(stubRepo.Object, stubLogger.Object);

            // Act            
            loader.Process(fileContents, "name", "description");

            // Assert 
            Assert.NotNull(loader.Loaded);
            Assert.NotNull(loader.Converted);
            Assert.Equal(2, loader.Converted.Items.Count());
        }

        [Fact]
        public void Process_ValidJson_SaveTosRepository()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.SaveAsync(It.IsNotNull<IInventory>()));
            var stubLogger = new Mock<ILogger<SoftwareAdviceLoader>>();
            var fileContents = GetProductList();
            var loader = new SoftwareAdviceLoader(mockRepo.Object, stubLogger.Object);

            // Act
            loader.Process(fileContents, "name", "description");

            // Assert 

            mockRepo.Verify(repo => repo.SaveAsync(loader.Converted), Times.Once);
        }


        /*********************
         * TestData
         *********************/
        string GetProductList()
        {
            return "{\r\n    \"products\": [\r\n        {\r\n            \"categories\": [\r\n                \"Customer Service\",\r\n                \"Call Center\"\r\n            ],\r\n            \"twitter\": \"@freshdesk\",\r\n            \"title\": \"Freshdesk\"\r\n        },\r\n        {\r\n            \"categories\": [\r\n                \"CRM\",\r\n                \"Sales Management\"\r\n            ],\r\n            \"title\": \"Zoho\"\r\n        }\r\n    ]\r\n}\r\n";
        }


    }
}
