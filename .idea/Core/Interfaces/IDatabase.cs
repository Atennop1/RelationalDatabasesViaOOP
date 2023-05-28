using System.Data;

namespace RelationalDatabasesViaOOP
{
    public interface IDatabase
    {
        IDataReader SendReaderRequest(string commandText);
        int SendNonQueryRequest(string commandText);
        object? SendScalarRequest(string commandText);
    
        Task<IDataReader> SendReaderRequestAsync(string commandText);
        Task<int> SendNonQueryRequestAsync(string commandText);
        Task<object?> SendScalarRequestAsync(string commandText);
    }
}