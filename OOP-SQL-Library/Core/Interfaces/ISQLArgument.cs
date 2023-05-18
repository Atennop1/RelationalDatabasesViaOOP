namespace LibrarySQL;

public interface ISQLArgument
{
    string Name { get; }
    object Value { get; }
}