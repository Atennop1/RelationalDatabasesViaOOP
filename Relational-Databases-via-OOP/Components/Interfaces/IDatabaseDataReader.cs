using System.Data;

namespace RelationalDatabasesViaOOP
{
    public interface IDatabaseDataReader
    {
        DataTable GetData(string tableName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!);
    }
}