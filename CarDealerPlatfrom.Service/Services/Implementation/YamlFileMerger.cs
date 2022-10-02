using CarDealerPlatform.Domain.Models;
using CarDealerPlatform.Service.Services.Interfaces;
using YamlDotNet.Serialization;

namespace CarDealerPlatform.Service.Services.Implementation
{
    public class YamlFileMerger : IFileMerger
    {
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public YamlFileMerger()
        {
            _serializer = new SerializerBuilder().Build();
            _deserializer = new DeserializerBuilder().Build();
        }

        public CarConfiguration Merge(Dictionary<string, object> mergeTo, List<Dictionary<string, object>> mergeFrom)
        {
            foreach (var dictionary in mergeFrom)
            {
                foreach (var mergeFromItem in dictionary)
                {
                    if (!mergeTo.ContainsKey(mergeFromItem.Key))
                    {
                        mergeTo.Add(mergeFromItem.Key, mergeFromItem.Value);
                        continue;
                    }

                    // not required for now
                    var mergeFromElements = mergeFromItem.Value as IEnumerable<object>;
                    if (mergeFromElements is not null)
                    {
                        var mergeToElements = mergeTo[mergeFromItem.Key] as IEnumerable<object>;
                        if (mergeToElements is not null)
                        {
                            var elements = mergeToElements.ToList();
                            elements.AddRange(mergeFromElements);
                            mergeTo[mergeFromItem.Key] = elements.Distinct();
                        }
                        else
                        {
                            // this case represents edge case and it should be discussed what is valid thing to do
                        }
                    }
                    else
                    {
                        mergeTo[mergeFromItem.Key] = mergeFromItem.Value;
                    }
                }
            }

            return _deserializer.Deserialize<CarConfiguration>(_serializer.Serialize(mergeTo));
        }
    }
}