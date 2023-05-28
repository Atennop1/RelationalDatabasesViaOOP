namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataDeleter
    {
        void DeleteData(string databaseName, IDatabaseValue[] valuesByWhichDeleting);
    }
}