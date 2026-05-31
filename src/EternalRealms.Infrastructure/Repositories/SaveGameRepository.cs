using EternalRealms.Application.DTOs;
using EternalRealms.Application.Interfaces;
using EternalRealms.Infrastructure.Persistence;
using EternalRealms.Infrastructure.Serialization;

namespace EternalRealms.Infrastructure.Repositories
{
    public sealed class SaveGameRepository : ISaveGameRepository
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IJsonSerializerService _jsonSerializerService;

        public SaveGameRepository(
            IFileStorageService fileStorageService,
            IJsonSerializerService jsonSerializerService)
        {
            _fileStorageService = fileStorageService;
            _jsonSerializerService = jsonSerializerService;
        }

        public void Save(SaveGameDto saveGameDto, string path)
        {
            var payload = _jsonSerializerService.Serialize(saveGameDto);
            _fileStorageService.Save(path, payload);
        }

        public SaveGameDto Load(string path)
        {
            var payload = _fileStorageService.Load(path);
            return _jsonSerializerService.Deserialize<SaveGameDto>(payload);
        }
    }
}
