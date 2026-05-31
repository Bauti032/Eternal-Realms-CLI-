namespace EternalRealms.Core.ValueObjects
{
    public sealed record Stats(int Strength, int Agility, int Intelligence, int Vitality, int Spirit)
    {
        public int ComputeHealth() => Vitality * 10 + Strength * 2;

        public int ComputeMana() => Spirit * 5 + Intelligence * 3;

        public int ComputePhysicalDamage() => Strength * 2 + Agility;

        public int ComputeMagicalDamage() => Intelligence * 2 + Spirit;
    }
}

