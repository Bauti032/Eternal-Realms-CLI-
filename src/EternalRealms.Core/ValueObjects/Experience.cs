namespace EternalRealms.Core.ValueObjects
{
    public sealed record Experience(int Amount)
    {
        public Experience Add(Experience other) => new(Amount + other.Amount);

        public static Experience Zero => new(0);
    }
}

