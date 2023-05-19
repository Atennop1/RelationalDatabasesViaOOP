namespace LibrarySQL
{
    public struct SQLArgument : ISQLArgument
    {
        public string Name { get; }
        public object Value { get; }

        public SQLArgument(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var stringValue = value as string;
            if (stringValue != null && string.IsNullOrEmpty(stringValue))
                throw new ArgumentException("String is null or empty");
                    
            if (stringValue != null && stringValue.IndexOfAny("&^\"\'@#$&|".ToCharArray()) != -1)
                throw new ArgumentException("Value contains forbidden symbols");
            
            Name = name ?? throw new ArgumentException("Name can't be null");
            Value = value is string ? $"'{value}'" : value;
        }
    }
}