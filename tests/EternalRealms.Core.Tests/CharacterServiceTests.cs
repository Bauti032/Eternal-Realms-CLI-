using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Enums;
using EternalRealms.Core.Events;
using EternalRealms.Core.Interfaces;
using EternalRealms.Core.Services;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Tests
{
    [TestFixture]
    public class CharacterServiceTests
    {
        [Test]
        public void Create_WithValidNameAndClass_ShouldInitializeBaseAttributes()
        {
            var character = Character.Create("Aldric", CharacterClass.Warrior);

            character.Name.Should().Be("Aldric");
            character.Level.Should().Be(1);
            character.Health.Current.Should().Be(character.Health.Maximum);
            character.Mana.Current.Should().Be(character.Mana.Maximum);
            character.IsAlive.Should().BeTrue();
        }

        [Test]
        public void LevelService_ApplyLevelUp_WhenExperienceThresholdReached_ShouldIncreaseLevelAndPublishEvent()
        {
            var eventBus = new TestEventBus();
            var levelService = new LevelService(eventBus);
            var character = Character.Create("Sera", CharacterClass.Mage);
            character.AddExperience(new Experience(100));

            levelService.ShouldLevelUp(character).Should().BeTrue();
            levelService.ApplyLevelUp(character);

            character.Level.Should().Be(2);
            eventBus.PublishedEvents.Should().ContainSingle().Which.Should().BeOfType<PlayerLevelUpEvent>();
            var published = (PlayerLevelUpEvent)eventBus.PublishedEvents[0];
            published.CharacterId.Should().Be(character.Id);
            published.NewLevel.Should().Be(2);
        }

        [Test]
        public void CombatService_ExecuteAttack_WhenEnemyDies_ShouldPublishEnemyKilledEvent()
        {
            var eventBus = new TestEventBus();
            var combatService = new CombatService(eventBus);
            var character = Character.Create("Ronan", CharacterClass.Ranger);
            var enemy = new Enemy(
                Guid.NewGuid(),
                "Rata venenosa",
                EnemyType.Beast,
                level: 1,
                Health.Create(1),
                new Damage(1, DamageType.Physical),
                new LootTable(Array.Empty<Item>(), new Reward(new Experience(5), new Gold(10))));

            var result = combatService.ExecuteAttack(character, enemy);

            result.IsEnemyDefeated.Should().BeTrue();
            result.DamageDealt.Should().Be(character.AttackPower);
            result.RemainingEnemyHealth.Should().Be(0);
            eventBus.PublishedEvents.Should().ContainSingle().Which.Should().BeOfType<EnemyKilledEvent>();
        }

        private sealed class TestEventBus : IEventBus
        {
            public List<object> PublishedEvents { get; } = new();

            public void Publish<TEvent>(TEvent @event)
                where TEvent : class
            {
                PublishedEvents.Add(@event!);
            }

            public void Subscribe<TEvent>(Action<TEvent> handler)
                where TEvent : class
            {
                // No-op for unit tests.
            }
        }
    }
}
