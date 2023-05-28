namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseValue
    {
        string Name { get; }
        object Get();
    }
}