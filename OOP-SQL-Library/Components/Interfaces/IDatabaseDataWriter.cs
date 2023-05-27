namespace LibrarySQL
{
    public interface IDatabaseDataWriter
    {
        void WriteData(string databaseName, IDatabaseValue[] valuesWhichWriting);
    }
}