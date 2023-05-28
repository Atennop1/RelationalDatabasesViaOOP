namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseParametersStringFactory
    {
        string Create(string[] names, string delimiter);
    }
}