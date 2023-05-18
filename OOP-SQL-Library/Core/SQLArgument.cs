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
            
            if (value is string valueString && (string.IsNullOrEmpty(valueString) || valueString.IndexOfAny("&^\"\'@#$&|".ToCharArray()) != -1))
                throw new ArgumentException("Value contains forbidden symbols");
            
            Name = name ?? throw new ArgumentException("Name can't be null");
            Value = value is string ? $"'{value}'" : value;
        }
    }
}