using CarDealerPlatform.Domain.Exceptions;
using CarDealerPlatform.Domain.Models;
using CarDealerPlatform.Service.Services.Implementation;
using CarDealerPlatform.Service.Services.Interfaces;
using System.Text.Json;
using Xunit;

namespace CarDealerPlatform.IntegrationTests
{
    public class CarServiceTests
    {
        private readonly ICarService _service;
        public CarServiceTests()
        {
            _service = new CarService(new YamlFileReader(), new YamlFileMerger());
        }

        [Fact]
        public async Task GetFullConfiguration_WhenCalled_ShouldReturnCorrectResult()
        {
            // Arange
            var expectedCarConfig = new CarConfiguration
            {
                manufacturer = "Mazda",
                model = "3",
                color = "Red",
                hp = 200,
                engine = 2.0M,
                fuel = "petrol",
            };

            // Act
            var actualCarConfig = await _service.GetFullConfigurationAsync("mazda", "3", 19832);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedCarConfig), JsonSerializer.Serialize(actualCarConfig));
        }

        [Theory]
        [InlineData("mazdaaa", "3", 19832)]
        [InlineData("mazda", "13", 19832)]
        [InlineData("mazda", "3", 198321)]
        public async Task GetFullConfiguration_FileDoesntExists_ThrowsException(string brand, string model, int offer)
        {
            // Arange
            // Act
            // Assert
            await Assert.ThrowsAsync<ConfigurationNotFoundException>(async () => await _service.GetFullConfigurationAsync(brand, model, offer));
        }
    }
}
