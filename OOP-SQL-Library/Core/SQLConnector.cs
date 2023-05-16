using System.Data;
using Npgsql;

namespace LibrarySQL
{
    public sealed class SQLConnector
    {
        private readonly NpgsqlConnection _sqlConnection;

        public SQLConnector(string authorizationString)
        {
            if (authorizationString == null)
                throw new ArgumentException("AuthorizationString can't be null");
            
            _sqlConnection = new NpgsqlConnection(authorizationString);
        }

        public NpgsqlConnection GetConnection()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            return _sqlConnection;
        }
    }
}