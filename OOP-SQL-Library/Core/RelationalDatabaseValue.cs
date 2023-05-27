namespace LibrarySQL
{
    public readonly struct RelationalDatabaseValue : IDatabaseValue
    {
        public string Name { get; }
        private readonly object _value;

        public RelationalDatabaseValue(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var stringValue = value as string;
            if (stringValue != null && string.IsNullOrEmpty(stringValue))
                throw new ArgumentException("String is null or empty");
                    
            if (stringValue != null && stringValue.IndexOfAny("&^\"\'@#$&|".ToCharArray()) != -1)
                throw new ArgumentException("Value contains forbidden symbols");
            
            Name = name ?? throw new ArgumentException("Name can't be null");
            _value = value is string ? $"'{value}'" : value;
        }

        public object Get()
            => _value;
    }
}