namespace LibrarySQL
{
    public interface ISQLDataWriter
    {
        void WriteData(string databaseName, ISQLArgument[] argumentsWhichWriting);
    }
}