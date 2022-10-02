using CarDealerPlatform.Service.Services.Interfaces;
using YamlDotNet.Serialization;

namespace CarDealerPlatform.Service.Services.Implementation
{
    public class YamlFileReader : IFileReader
    {
        private readonly IDeserializer _deserializer;
        public YamlFileReader()
        {
            _deserializer = new DeserializerBuilder().Build();
        }

        public async Task<Dictionary<string, object>?> GetFileAsync(string path)
        {
            if (!File.Exists(path))
                return null;

            var file = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), path));
            return _deserializer.Deserialize<Dictionary<string, object>>(file);
        }
    }
}
