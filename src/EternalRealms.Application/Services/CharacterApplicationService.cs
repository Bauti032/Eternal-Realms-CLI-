using System.Linq;
using EternalRealms.Application.DTOs;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Services
{
    public sealed class CharacterApplicationService
    {
        public CharacterDto MapToDto(Character character)
        {
            return new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                CharacterClass = character.CharacterClass,
                Level = character.Level,
                Stats = character.Stats,
                Health = character.Health,
                Mana = character.Mana,
                Gold = character.Gold,
                Experience = character.Experience,
                Inventory = character.Inventory.Items.Select(MapItem).ToList(),
                Equipment = character.Equipment.Slots.ToDictionary(pair => pair.Key, pair => pair.Value is null ? null : MapItem(pair.Value)),
                ActiveQuest = character.ActiveQuest is null ? null : MapQuest(character.ActiveQuest)
            };
        }

        private static ItemDto MapItem(Item item)
        {
            return item switch
            {
                Weapon weapon => new ItemDto
                {
                    Id = weapon.Id,
                    Name = weapon.Name,
                    Rarity = weapon.Rarity,
                    Weight = weapon.Weight,
                    ItemType = nameof(Weapon),
                    Damage = weapon.Damage,
                    Slot = weapon.Slot
                },
                Armor armor => new ItemDto
                {
                    Id = armor.Id,
                    Name = armor.Name,
                    Rarity = armor.Rarity,
                    Weight = armor.Weight,
                    ItemType = nameof(Armor),
                    Defense = armor.Defense,
                    Slot = armor.Slot
                },
                Consumable consumable => new ItemDto
                {
                    Id = consumable.Id,
                    Name = consumable.Name,
                    Rarity = consumable.Rarity,
                    Weight = consumable.Weight,
                    ItemType = nameof(Consumable),
                    HealthRestoration = consumable.HealthRestoration,
                    ManaRestoration = consumable.ManaRestoration
                },
                _ => new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Rarity = item.Rarity,
                    Weight = item.Weight,
                    ItemType = item.GetType().Name
                }
            };
        }

        private static QuestDto MapQuest(Quest quest)
        {
            return new QuestDto
            {
                Id = quest.Id,
                Title = quest.Title,
                Description = quest.Description,
                Status = quest.Status,
                RequiredLevel = quest.RequiredLevel,
                Reward = quest.Reward
            };
        }
    }
}

