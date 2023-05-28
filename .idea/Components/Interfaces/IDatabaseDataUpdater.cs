namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataUpdater
    {
        void UpdateData(string databaseName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesWhichChanging);
    }
}