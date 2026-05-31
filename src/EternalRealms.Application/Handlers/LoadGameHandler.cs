using System.Linq;
using EternalRealms.Application.Commands;
using EternalRealms.Application.DTOs;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.Handlers
{
    public sealed class LoadGameHandler
    {
        private readonly ISaveGameRepository _saveGameRepository;
        private readonly ICharacterRepository _characterRepository;

        public LoadGameHandler(
            ISaveGameRepository saveGameRepository,
            ICharacterRepository characterRepository)
        {
            _saveGameRepository = saveGameRepository;
            _characterRepository = characterRepository;
        }

        public SaveGameDto Handle(LoadGameCommand command)
        {
            var saveGame = _saveGameRepository.Load(command.Path);
            var character = MapCharacter(saveGame.Character);
            _characterRepository.Save(character);
            return saveGame;
        }

        private static Character MapCharacter(CharacterDto dto)
        {
            var inventory = new Inventory(20);
            foreach (var itemDto in dto.Inventory)
            {
                inventory.AddItem(MapItem(itemDto));
            }

            var equipment = new Equipment();
            foreach (var slotPair in dto.Equipment)
            {
                if (slotPair.Value is null)
                {
                    continue;
                }

                equipment.Equip(MapItem(slotPair.Value));
            }

            var activeQuest = dto.ActiveQuest is null
                ? null
                : Quest.Restore(dto.ActiveQuest.Id, dto.ActiveQuest.Title, dto.ActiveQuest.Description, dto.ActiveQuest.RequiredLevel, dto.ActiveQuest.Reward, dto.ActiveQuest.Status);

            return Character.Restore(
                dto.Id,
                dto.Name,
                dto.CharacterClass,
                dto.Level,
                dto.Stats,
                dto.Health,
                dto.Mana,
                inventory,
                equipment,
                dto.Gold,
                dto.Experience,
                activeQuest);
        }

        private static Item MapItem(ItemDto dto)
        {
            return dto.ItemType switch
            {
                nameof(Weapon) => new Weapon(dto.Name, dto.Rarity, dto.Weight, dto.Damage ?? new Damage(0, DamageType.Physical)),
                nameof(Armor) => new Armor(dto.Name, dto.Rarity, dto.Weight, dto.Slot ?? EquipmentSlot.Head, dto.Defense ?? 0),
                nameof(Consumable) => new Consumable(dto.Name, dto.Rarity, dto.Weight, dto.HealthRestoration ?? 0, dto.ManaRestoration ?? 0),
                _ => new Weapon(dto.Name, dto.Rarity, dto.Weight, dto.Damage ?? new Damage(0, DamageType.Physical)),
            };
        }
    }
}

