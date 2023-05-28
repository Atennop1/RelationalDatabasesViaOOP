using System;
using System.Data;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class RelationalDatabaseDataReader : IDatabaseDataReader
    {
        private readonly IDatabase _database;
        private readonly IDatabaseParametersStringFactory _databaseParametersStringFactory;

        public RelationalDatabaseDataReader(IDatabase database, IDatabaseParametersStringFactory databaseParametersStringFactory)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _databaseParametersStringFactory = databaseParametersStringFactory ?? throw new ArgumentNullException(nameof(databaseParametersStringFactory));
        }

        public DataTable GetData(string databaseName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (columnsNames == null)
                throw new ArgumentNullException(nameof(columnsNames));

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            stringBuilder.Append(columnsNames.Length == 0 ? "*" : _databaseParametersStringFactory.Create(columnsNames, ", "));
            stringBuilder.Append($" FROM {databaseName}");

            if (valuesByWhichSelecting != null && valuesByWhichSelecting.Length != 0)
            {
                stringBuilder.Append(" WHERE ");
                stringBuilder.Append(_databaseParametersStringFactory.Create(valuesByWhichSelecting.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            }
            
            var dataReader = _database.SendReaderRequest(stringBuilder.ToString());
            var dataTable = new DataTable();
            
            dataTable.Load(dataReader);
            return dataTable;
        }
    }
}