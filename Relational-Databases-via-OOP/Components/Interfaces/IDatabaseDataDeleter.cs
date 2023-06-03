namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataDeleter
    {
        void DeleteData(string tableName, IDatabaseValue[] valuesByWhichDeleting);
    }
}