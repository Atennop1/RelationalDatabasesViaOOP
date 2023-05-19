using System.Data;

namespace LibrarySQL
{
    public interface ISQLDataReader
    {
        DataTable GetData(string databaseName, string[] columnsNames, SQLArgument[] argumentsByWhichSelecting);
    }
}