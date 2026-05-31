using System;
using System.IO;
using EternalRealms.Application.Interfaces;
using EternalRealms.Infrastructure.Persistence;

namespace EternalRealms.Infrastructure.Logging
{
    public sealed class FileLogger : ILogger
    {
        private readonly string _path;

        public FileLogger(IFileStorageService fileStorageService, string path)
        {
            _path = path;
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                fileStorageService.Save(path, string.Empty);
            }
        }

        public void Info(string message)
        {
            Write("INFO", message);
        }

        public void Error(string message, Exception? exception = null)
        {
            var details = exception is null ? message : $"{message}\n{exception}";
            Write("ERROR", details);
        }

        private void Write(string level, string text)
        {
            var line = $"[{DateTime.UtcNow:O}] {level}: {text}{Environment.NewLine}";
            File.AppendAllText(_path, line);
        }
    }
}
