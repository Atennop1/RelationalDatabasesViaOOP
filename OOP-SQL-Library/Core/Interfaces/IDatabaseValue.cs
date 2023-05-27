namespace LibrarySQL
{
    public interface IDatabaseValue
    {
        string Name { get; }
        object Get();
    }
}