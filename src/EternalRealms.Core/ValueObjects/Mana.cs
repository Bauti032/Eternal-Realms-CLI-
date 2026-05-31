namespace EternalRealms.Core.ValueObjects
{
    public sealed record Mana(int Current, int Maximum)
    {
        public bool IsEmpty => Current <= 0;

        public Mana Consume(int amount)
        {
            var next = Math.Max(Current - Math.Max(amount, 0), 0);
            return this with { Current = next };
        }

        public Mana Restore(int amount)
        {
            var next = Math.Min(Current + Math.Max(amount, 0), Maximum);
            return this with { Current = next };
        }

        public Mana RestoreFully() => this with { Current = Maximum };

        public static Mana Create(int maximum) => new(maximum, maximum);
    }
}

