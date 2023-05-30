using System;

namespace RelationalDatabasesViaOOP
{
    public class RelationalDatabaseValue : IDatabaseValue
    {
        public string Name { get; }
        private readonly object _value;

        public RelationalDatabaseValue(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (value as string is { } stringValue && stringValue.IndexOf("'", StringComparison.Ordinal) != -1)
                value = stringValue.Replace("'", "''");
            
            if (name.IndexOf("'", StringComparison.Ordinal) != -1)
                name = name.Replace("'", "''");
            
            _value = value is string ? $"'{value}'" : value;
            Name = name;
        }

        public object Get()
            => _value;
    }
}