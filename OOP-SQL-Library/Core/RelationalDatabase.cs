using System.Data;
using Npgsql;

namespace LibrarySQL
{
    public sealed class RelationalDatabase : IDatabase
    {
        private readonly NpgsqlConnection _sqlConnection;
        
        private NpgsqlConnection SQLConnection
        {
            get
            {
                if (_sqlConnection.State != ConnectionState.Open)
                    _sqlConnection.Open();

                return _sqlConnection;
            }
        }

        public RelationalDatabase(string authorizationString)
        {
            if (authorizationString == null)
                throw new ArgumentException("AuthorizationString can't be null");
            
            _sqlConnection = new NpgsqlConnection(authorizationString);
        }

        public IDataReader SendReaderRequest(string commandText)
        {
            if (commandText == null)
                throw new ArgumentNullException(nameof(commandText));
            
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return command.ExecuteReader();
        }

        public int SendNonQueryRequest(string commandText)
        {
            if (commandText == null)
                throw new ArgumentNullException(nameof(commandText));
            
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return command.ExecuteNonQuery();
        }

        public object? SendScalarRequest(string commandText)
        {
            if (commandText == null)
                throw new ArgumentNullException(nameof(commandText));
            
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return command.ExecuteScalar();
        }
        
        public Task<IDataReader> SendReaderRequestAsync(string commandText)
        {
            if (commandText == null)
                throw new ArgumentNullException(nameof(commandText));
            
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return Task.FromResult((IDataReader)command.ExecuteReader());
        }
        
        public Task<int> SendNonQueryRequestAsync(string commandText)
        {
            if (commandText == null)
                throw new ArgumentNullException(nameof(commandText));
            
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return Task.FromResult(command.ExecuteNonQuery());
        }

        public Task<object?> SendScalarRequestAsync(string commandText)
        {
            if (commandText == null)
                throw new ArgumentNullException(nameof(commandText));
            
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return Task.FromResult(command.ExecuteScalar());
        }
    }
}