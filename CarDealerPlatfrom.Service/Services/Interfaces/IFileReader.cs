namespace CarDealerPlatform.Service.Services.Interfaces
{
    public interface IFileReader
    {
        Task<Dictionary<string, object>?> GetFileAsync(string path);
    }
}
