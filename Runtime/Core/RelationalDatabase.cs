#nullable enable
using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Realisation of <b>IDatabase</b> interface which is relational database
    /// </summary>
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

        public DataTable SendReaderRequest(string commandText)
        {
            var command = new NpgsqlCommand(commandText, SQLConnection);

            var dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            return dataTable;
        }

        public int SendNonQueryRequest(string commandText)
        {
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return command.ExecuteNonQuery();
        }

        public object? SendScalarRequest(string commandText)
        {
            var command = new NpgsqlCommand(commandText, SQLConnection);
            return command.ExecuteScalar();
        }
        
        public Task<DataTable> SendReaderRequestAsync(string commandText) 
            => Task.FromResult(SendReaderRequest(commandText));

        public Task<int> SendNonQueryRequestAsync(string commandText) 
            => Task.FromResult(SendNonQueryRequest(commandText));

        public Task<object?> SendScalarRequestAsync(string commandText) 
            => Task.FromResult(SendScalarRequest(commandText));
    }
}