using System.Text.Json;
using System.Text.Json.Serialization;

namespace EternalRealms.Infrastructure.Serialization
{
    public interface IJsonSerializerService
    {
        string Serialize<T>(T input);
        T Deserialize<T>(string payload);
    }

    public sealed class JsonSerializerService : IJsonSerializerService
    {
        private readonly JsonSerializerOptions _options;

        public JsonSerializerService()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public string Serialize<T>(T input)
        {
            return JsonSerializer.Serialize(input, _options);
        }

        public T Deserialize<T>(string payload)
        {
            return JsonSerializer.Deserialize<T>(payload, _options)!;
        }
    }
}
