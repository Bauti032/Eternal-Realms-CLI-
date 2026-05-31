namespace EternalRealms.Core.ValueObjects
{
    public sealed record Health(int Current, int Maximum)
    {
        public bool IsDead => Current <= 0;

        public Health TakeDamage(int amount)
        {
            var next = Math.Max(Current - Math.Max(amount, 0), 0);
            return this with { Current = next };
        }

        public Health Heal(int amount)
        {
            var next = Math.Min(Current + Math.Max(amount, 0), Maximum);
            return this with { Current = next };
        }

        public Health RestoreFully() => this with { Current = Maximum };

        public static Health Create(int maximum) => new(maximum, maximum);
    }
}

