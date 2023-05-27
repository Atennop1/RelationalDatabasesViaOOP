using System.Data;

namespace LibrarySQL
{
    public interface IDatabase
    {
        IDataReader ExecuteReaderCommand(string commandText);
        int ExecuteNonQueryCommand(string commandText);
        object? ExecuteScalarCommand(string commandText);
    
        Task<IDataReader> ExecuteReaderCommandAsync(string commandText);
        Task<int> ExecuteNonQueryCommandAsync(string commandText);
        Task<object?> ExecuteScalarCommandAsync(string commandText);
    }
}