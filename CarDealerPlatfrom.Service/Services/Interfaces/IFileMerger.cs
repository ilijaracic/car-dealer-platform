using CarDealerPlatform.Domain.Models;

namespace CarDealerPlatform.Service.Services.Interfaces
{
    public interface IFileMerger
    {
        CarConfiguration Merge(Dictionary<string, object> mergetTo, List<Dictionary<string, object>> mergeFrom);
    }
}
