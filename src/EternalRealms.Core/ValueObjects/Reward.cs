namespace EternalRealms.Core.ValueObjects
{
    public sealed record Reward(Experience Experience, Gold Gold)
    {
        public static Reward Create(Experience experience, Gold gold) => new(experience, gold);
    }
}

