using System;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class EnumerationStringFactory : IEnumerationStringFactory
    {
        public string Create(string[] strings, string delimiter)
        {
            if (strings == null)
                throw new ArgumentNullException(nameof(strings));

            if (delimiter == null)
                throw new ArgumentNullException(nameof(delimiter));
            
            if (strings.Length == 0)
                return string.Empty;

            var stringBuilder = new StringBuilder();
            foreach (var name in strings)
            {
                stringBuilder.Append(name);
                stringBuilder.Append(name != strings[strings.Length - 1] ? delimiter : string.Empty);
            }

            return stringBuilder.ToString();
        }
    }
}