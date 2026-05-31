using System.IO;
using EternalRealms.Application.DTOs;

namespace EternalRealms.Application.Validators
{
    public sealed class SaveGameValidator
    {
        public void Validate(SaveGameDto saveGameDto)
        {
            if (saveGameDto.Character is null)
            {
                throw new InvalidDataException("La partida guardada debe contener un personaje.");
            }
        }

        public void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("La ruta de guardado no puede estar vacía.", nameof(path));
            }
        }
    }
}

