using EternalRealms.Core.Enums;
using EternalRealms.Core.Events;
using EternalRealms.Core.Exceptions;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Entities
{
    public sealed class Character
    {
        private Character(
            Guid id,
            string name,
            CharacterClass characterClass,
            int level,
            Stats stats,
            Health health,
            Mana mana,
            Inventory inventory,
            Equipment equipment,
            Gold gold,
            Experience experience)
        {
            Id = id;
            Name = name;
            CharacterClass = characterClass;
            Level = level;
            Stats = stats;
            Health = health;
            Mana = mana;
            Inventory = inventory;
            Equipment = equipment;
            Gold = gold;
            Experience = experience;
        }

        public Guid Id { get; init; }
        public string Name { get; private set; }
        public CharacterClass CharacterClass { get; private set; }
        public int Level { get; private set; }
        public Stats Stats { get; private set; }
        public Health Health { get; private set; }
        public Mana Mana { get; private set; }
        public Inventory Inventory { get; private set; }
        public Equipment Equipment { get; private set; }
        public Gold Gold { get; private set; }
        public Experience Experience { get; private set; }
        public Quest? ActiveQuest { get; private set; }

        public int AttackPower
        {
            get
            {
                if (Equipment.GetEquippedItem(EquipmentSlot.Weapon) is Weapon weapon)
                {
                    return weapon.Damage.Value + Stats.ComputePhysicalDamage();
                }

                return Stats.ComputePhysicalDamage();
            }
        }

        public bool IsAlive => !Health.IsDead;

        public static Character Create(string name, CharacterClass characterClass)
        {
            var stats = characterClass switch
            {
                CharacterClass.Warrior => new Stats(10, 5, 2, 10, 3),
                CharacterClass.Mage => new Stats(3, 5, 10, 5, 10),
                CharacterClass.Ranger => new Stats(6, 10, 4, 7, 5),
                CharacterClass.Rogue => new Stats(5, 12, 3, 6, 4),
                CharacterClass.Cleric => new Stats(4, 6, 7, 8, 9),
                _ => new Stats(5, 5, 5, 5, 5),
            };

            var health = Health.Create(stats.ComputeHealth());
            var mana = Mana.Create(stats.ComputeMana());

            return new Character(
                Guid.NewGuid(),
                name,
                characterClass,
                level: 1,
                stats,
                health,
                mana,
                new Inventory(capacity: 20),
                new Equipment(),
                Gold.Zero,
                Experience.Zero);
        }

        public static Character Restore(
            Guid id,
            string name,
            CharacterClass characterClass,
            int level,
            Stats stats,
            Health health,
            Mana mana,
            Inventory inventory,
            Equipment equipment,
            Gold gold,
            Experience experience,
            Quest? activeQuest)
        {
            var character = new Character(
                id,
                name,
                characterClass,
                level,
                stats,
                health,
                mana,
                inventory,
                equipment,
                gold,
                experience);

            character.ActiveQuest = activeQuest;
            return character;
        }

        public void AddExperience(Experience experience)
        {
            Experience = Experience.Add(experience);
        }

        public void AddGold(Gold gold)
        {
            Gold = Gold.Add(gold);
        }

        public void LevelUp()
        {
            Level++;
            Stats = CharacterClass switch
            {
                CharacterClass.Warrior => Stats with { Strength = Stats.Strength + 2, Vitality = Stats.Vitality + 2, Agility = Stats.Agility + 1 },
                CharacterClass.Mage => Stats with { Intelligence = Stats.Intelligence + 2, Spirit = Stats.Spirit + 2, Agility = Stats.Agility + 1 },
                CharacterClass.Ranger => Stats with { Agility = Stats.Agility + 2, Strength = Stats.Strength + 1, Vitality = Stats.Vitality + 1 },
                CharacterClass.Rogue => Stats with { Agility = Stats.Agility + 3, Strength = Stats.Strength + 1, Spirit = Stats.Spirit + 1 },
                CharacterClass.Cleric => Stats with { Spirit = Stats.Spirit + 2, Vitality = Stats.Vitality + 2, Intelligence = Stats.Intelligence + 1 },
                _ => Stats with { Strength = Stats.Strength + 1, Agility = Stats.Agility + 1, Vitality = Stats.Vitality + 1, Intelligence = Stats.Intelligence + 1, Spirit = Stats.Spirit + 1 },
            };

            Health = Health.Create(Stats.ComputeHealth());
            Mana = Mana.Create(Stats.ComputeMana());
        }

        public void TakeDamage(int amount)
        {
            Health = Health.TakeDamage(amount);

            if (Health.IsDead)
            {
                throw new CharacterDeadException();
            }
        }

        public void Heal(int amount)
        {
            Health = Health.Heal(amount);
        }

        public void RestoreMana(int amount)
        {
            Mana = Mana.Restore(amount);
        }

        public void EquipItem(Item item)
        {
            if (item is not Armor and not Weapon)
            {
                throw new InvalidEquipmentException("Solo se pueden equipar armas o armaduras.");
            }

            Equipment.Equip(item);
        }

        public void UseConsumable(Consumable consumable)
        {
            if (!Inventory.Contains(consumable.Id))
            {
                throw new ItemNotFoundException(consumable.Name);
            }

            Heal(consumable.HealthRestoration);
            RestoreMana(consumable.ManaRestoration);
            Inventory.RemoveItem(consumable.Id);
        }

        public void AssignQuest(Quest quest)
        {
            if (ActiveQuest is not null && ActiveQuest.Status == QuestStatus.Active)
            {
                throw new InvalidQuestException("Ya existe una misión activa.");
            }

            quest.Accept(this);
            ActiveQuest = quest;
        }

        public void CompleteQuest()
        {
            if (ActiveQuest is null)
            {
                throw new InvalidQuestException("No hay una misión activa para completar.");
            }

            ActiveQuest.Complete();
            AddExperience(ActiveQuest.Reward.Experience);
            AddGold(ActiveQuest.Reward.Gold);
            ActiveQuest = null;
        }
    }
}

