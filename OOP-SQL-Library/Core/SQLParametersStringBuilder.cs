using System.Text;

namespace LibrarySQL
{
    public sealed class SQLParametersStringBuilder : ISQLParametersStringBuilder
    {
        public string BuildParameters(string[] names, string delimiter)
        {
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