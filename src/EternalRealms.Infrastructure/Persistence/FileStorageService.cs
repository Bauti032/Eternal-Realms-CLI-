using System.IO;

namespace EternalRealms.Infrastructure.Persistence
{
    public interface IFileStorageService
    {
        void Save(string path, string content);
        string Load(string path);
        bool Exists(string path);
    }

    public sealed class FileStorageService : IFileStorageService
    {
        public void Save(string path, string content)
        {
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, content);
        }

        public string Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"El archivo de guardado no se encontró: {path}");
            }

            return File.ReadAllText(path);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
