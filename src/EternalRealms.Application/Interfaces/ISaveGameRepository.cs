using EternalRealms.Application.DTOs;

namespace EternalRealms.Application.Interfaces
{
    public interface ISaveGameRepository
    {
        void Save(SaveGameDto saveGameDto, string path);
        SaveGameDto Load(string path);
    }
}

