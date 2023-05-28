using System.Data;

namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataReader
    {
        DataTable GetData(string databaseName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!);
    }
}