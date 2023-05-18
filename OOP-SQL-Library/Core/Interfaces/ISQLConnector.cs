using Npgsql;

namespace LibrarySQL;

public interface ISQLConnector
{
    NpgsqlConnection GetConnection();
}