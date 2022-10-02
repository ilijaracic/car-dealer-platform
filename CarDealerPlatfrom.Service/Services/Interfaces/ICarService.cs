using CarDealerPlatform.Domain.Models;

namespace CarDealerPlatform.Service.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarConfiguration> GetFullConfigurationAsync(string brand, string model, int offer);
    }
}
