using System.Data;

namespace LibrarySQL
{
    public interface IDatabaseDataReader
    {
        DataTable GetData(string databaseName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!);
    }
}