using System.Data;

namespace LibrarySQL;

public interface ISQLDataReader
{
    DataTable GetData(string sqlRequest);
}