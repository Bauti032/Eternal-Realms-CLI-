using EternalRealms.Infrastructure.Persistence;
using EternalRealms.Infrastructure.Serialization;

namespace EternalRealms.Infrastructure.Configuration
{
    public sealed class ConfigurationLoader
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IJsonSerializerService _jsonSerializerService;

        public ConfigurationLoader(
            IFileStorageService fileStorageService,
            IJsonSerializerService jsonSerializerService)
        {
            _fileStorageService = fileStorageService;
            _jsonSerializerService = jsonSerializerService;
        }

        public GameSettings Load(string path)
        {
            if (!_fileStorageService.Exists(path))
            {
                return new GameSettings();
            }

            var payload = _fileStorageService.Load(path);
            return _jsonSerializerService.Deserialize<GameSettings>(payload);
        }

        public void Save(GameSettings settings, string path)
        {
            var payload = _jsonSerializerService.Serialize(settings);
            _fileStorageService.Save(path, payload);
        }
    }
}
