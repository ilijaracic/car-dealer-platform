using CarDealerPlatform.Domain.Exceptions;
using CarDealerPlatform.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CarDealerPlatfromFunction
{
    public class IPHttpTrigger
    {
        private readonly ICarService _service;

        public IPHttpTrigger(ICarService service)
        {
            _service = service;
        }

        [FunctionName("IP")]
        public async Task<IActionResult> Run(
     [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
     ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string brand = req.Query["brand"];
                string model = req.Query["model"];

                if (!int.TryParse(req.Query["offer"], out int offer))
                {
                    return new BadRequestObjectResult("Offer must be a number");
                }

                return new OkObjectResult(await _service.GetFullConfigurationAsync(brand, model, offer));
            }
            catch (ConfigurationNotFoundException ex)
            {
                log.LogError(ex.Message, ex);

                return new BadRequestObjectResult(ex.Message);
            }

            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);

                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = "Something went wrong!"
                };
            }
        }
    }
}
