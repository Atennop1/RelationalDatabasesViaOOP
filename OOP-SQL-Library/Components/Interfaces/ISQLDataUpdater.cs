namespace LibrarySQL
{
    public interface ISQLDataUpdater
    {
        void UpdateData(string databaseName, ISQLArgument[] argumentsThatChanging, ISQLArgument[] argumentsByWhichChanging);
    }
}