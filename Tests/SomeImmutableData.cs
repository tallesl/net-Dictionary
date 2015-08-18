namespace PropertiesHash.Tests
{
    using System;

    public class SomeImmutableData
    {
        public readonly Guid Id;

        public readonly int Integer;

        public readonly string Text;

        public readonly DateTime? Date;

        public SomeImmutableData(Guid id, int integer, string text, DateTime? date)
        {
            Id = id;
            Integer = integer;
            Text = text;
            Date = date;
        }
    }
}
