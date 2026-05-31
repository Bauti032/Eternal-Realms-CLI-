namespace EternalRealms.Core.ValueObjects
{
    public sealed record Gold(int Amount)
    {
        public Gold Add(Gold other) => new(Amount + other.Amount);

        public Gold Subtract(Gold other)
        {
            return new(Math.Max(Amount - other.Amount, 0));
        }

        public static Gold Zero => new(0);
    }
}

