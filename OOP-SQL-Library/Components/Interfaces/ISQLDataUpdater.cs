namespace LibrarySQL
{
    public interface ISQLDataUpdater
    {
        void UpdateData(string databaseName, ISQLArgument[] replacedArguments, ISQLArgument[] argumentsWhichChanging);
    }
}