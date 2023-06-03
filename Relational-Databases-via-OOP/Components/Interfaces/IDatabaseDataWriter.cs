namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataWriter
    {
        void WriteData(string tableName, IDatabaseValue[] valuesWhichWriting);
    }
}