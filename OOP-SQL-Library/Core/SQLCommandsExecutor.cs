using Npgsql;

namespace LibrarySQL
{
    public sealed class SQLCommandsExecutor : ISQLCommandsExecutor
    {
        private readonly ISQLConnector _connector;

        public SQLCommandsExecutor(ISQLConnector connector) 
            => _connector = connector ?? throw new ArgumentException("Connector can't be null");

        public NpgsqlDataReader ExecuteReader(string commandText)
        {
            var command = new NpgsqlCommand(commandText, _connector.GetConnection());
            return command.ExecuteReader();
        }

        public int ExecuteNonQuery(string commandText)
        {
            var command = new NpgsqlCommand(commandText, _connector.GetConnection());
            return command.ExecuteNonQuery();
        }

        public object ExecuteScalar(string commandText)
        {
            var command = new NpgsqlCommand(commandText, _connector.GetConnection());
            return command.ExecuteScalar();
        }
        
        public Task<NpgsqlDataReader> ExecuteReaderAsync(string commandText)
        {
            var command = new NpgsqlCommand(commandText, _connector.GetConnection());
            return Task.FromResult(command.ExecuteReader());
        }
        
        public Task<int> ExecuteNonQueryAsync(string commandText)
        {
            var command = new NpgsqlCommand(commandText, _connector.GetConnection());
            return Task.FromResult(command.ExecuteNonQuery());
        }

        public Task<object> ExecuteScalarAsync(string commandText)
        {
            var command = new NpgsqlCommand(commandText, _connector.GetConnection());
            return Task.FromResult(command.ExecuteScalar());
        }
    }
}