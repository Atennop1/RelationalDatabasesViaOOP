using System;

namespace RelationalDatabasesViaOOP
{
    public readonly struct RelationalDatabaseValue : IDatabaseValue
    {
        public string Name { get; }
        private readonly object _value;

        public RelationalDatabaseValue(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value as string is { } stringValue && stringValue.IndexOfAny("\"\'".ToCharArray()) != -1)
                value = stringValue.Replace("\'", "\\\'");
            
            Name = name ?? throw new ArgumentException("Name can't be null");
            _value = value is string ? $"'{value}'" : value;
        }

        public object Get()
            => _value;
    }
}