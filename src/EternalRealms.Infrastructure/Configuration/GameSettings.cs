namespace EternalRealms.Infrastructure.Configuration
{
    public sealed record GameSettings
    {
        public string SaveDirectory { get; init; } = "saves";
        public string LogFilePath { get; init; } = "logs/game.log";
        public int DefaultInventoryCapacity { get; init; } = 20;
    }
}
