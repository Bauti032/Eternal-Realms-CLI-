using EternalRealms.Core.Enums;

namespace EternalRealms.Core.ValueObjects
{
    public sealed record Damage(int Value, DamageType Type)
    {
        public static Damage Physical(int value) => new(value, DamageType.Physical);

        public static Damage Magical(int value) => new(value, DamageType.Magical);
    }
}

