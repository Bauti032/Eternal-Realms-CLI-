using System;
using System.Collections.Generic;

namespace EternalRealms.Application.DTOs
{
    public sealed class SaveGameDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime SavedAt { get; init; } = DateTime.UtcNow;
        public CharacterDto Character { get; init; } = default!;
        public IEnumerable<QuestDto> Quests { get; init; } = Array.Empty<QuestDto>();
    }
}

