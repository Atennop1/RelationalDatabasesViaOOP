using System.Text;

namespace LibrarySQL
{
    public sealed class SQLParametersStringFactory : ISQLParametersStringFactory
    {
        public string Create(string[] names, string delimiter)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names));

            if (delimiter == null)
                throw new ArgumentNullException(nameof(delimiter));
            
            if (names.Length == 0)
                return string.Empty;

            var stringBuilder = new StringBuilder();
            foreach (var name in names)
            {
                stringBuilder.Append(name);
                stringBuilder.Append(name != names[^1] ? delimiter : string.Empty);
            }

            return stringBuilder.ToString();
        }
    }
}