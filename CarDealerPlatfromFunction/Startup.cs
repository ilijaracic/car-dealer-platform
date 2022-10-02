
using CarDealerPlatform.Service.Services.Implementation;
using CarDealerPlatform.Service.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CarDealerPlatfromFunction.Startup))]

namespace CarDealerPlatfromFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ICarService, CarService>();
            builder.Services.AddTransient<IFileReader, YamlFileReader>();
            builder.Services.AddTransient<IFileMerger, YamlFileMerger>();
        }
    }
}
