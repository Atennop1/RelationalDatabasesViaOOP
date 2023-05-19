namespace LibrarySQL
{
    public interface ISQLDataDeleter
    {
        void DeleteData(string databaseName, ISQLArgument[] sqlArguments);
    }
}