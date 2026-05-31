using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.DTOs
{
    public sealed class QuestDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public QuestStatus Status { get; init; }
        public int RequiredLevel { get; init; }
        public Reward Reward { get; init; } = default!;
    }
}

