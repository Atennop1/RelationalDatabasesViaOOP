namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataUpdater
    {
        void UpdateData(string tableName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesWhichChanging);
    }
}