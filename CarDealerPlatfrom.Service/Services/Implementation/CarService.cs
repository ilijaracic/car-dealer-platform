using CarDealerPlatform.Domain.Exceptions;
using CarDealerPlatform.Domain.Models;
using CarDealerPlatform.Service.Services.Interfaces;

namespace CarDealerPlatform.Service.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IFileReader _fileReader;
        private readonly IFileMerger _fileMerger;

        public CarService(IFileReader fileReader, IFileMerger fileMerger)
        {
            _fileReader = fileReader;
            _fileMerger = fileMerger;
        }

        public async Task<CarConfiguration> GetFullConfigurationAsync(string brand, string model, int offer)
        {
            var brandDictionary = await _fileReader.GetFileAsync(@"Assets\Brands\" + brand + ".yaml");
            if (brandDictionary is null)
            {
                throw new ConfigurationNotFoundException($"Brand: {brand} not found");
            }

            var modelDictionary = await _fileReader.GetFileAsync(@"Assets\Models\" + model + ".yaml");
            if (modelDictionary is null)
            {
                throw new ConfigurationNotFoundException($"Model: {model} not found");
            }

            var offerDictionary = await _fileReader.GetFileAsync(@"Assets\Offers\" + offer + ".yaml");
            if (offerDictionary is null)
            {
                throw new ConfigurationNotFoundException($"Offer: {offer} not found");
            }

            var mergeFrom = new List<Dictionary<string, object>>
            {
                modelDictionary,
                offerDictionary
            };

            return _fileMerger.Merge(brandDictionary, mergeFrom);
        }
    }
}
