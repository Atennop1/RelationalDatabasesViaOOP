namespace LibrarySQL;

public interface ISQLParametersStringBuilder
{
    string BuildParameters(string[] names, string delimiter);
}