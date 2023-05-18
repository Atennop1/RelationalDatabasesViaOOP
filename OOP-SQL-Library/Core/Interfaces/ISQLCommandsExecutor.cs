using Npgsql;

namespace LibrarySQL;

public interface ISQLCommandsExecutor
{
    NpgsqlDataReader ExecuteReader(string commandText);
    int ExecuteNonQuery(string commandText);
    object ExecuteScalar(string commandText);
    Task<NpgsqlDataReader> ExecuteReaderAsync(string commandText);
    Task<int> ExecuteNonQueryAsync(string commandText);
    Task<object> ExecuteScalarAsync(string commandText);
}