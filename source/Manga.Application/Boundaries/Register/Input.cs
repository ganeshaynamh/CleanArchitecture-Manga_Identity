namespace Manga.Application.Boundaries.Register
{
    using Manga.Domain.ValueObjects;

    public sealed class Input
    {
        public string SSN { get; }
        public string Name { get; }
        public PositiveAmount InitialAmount { get; }
        
        public Input(string ssn, string name, PositiveAmount initialAmount)
        {
            SSN = ssn;
            Name = name;
            InitialAmount = initialAmount;
        }
    }
}