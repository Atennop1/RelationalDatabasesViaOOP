using System;
using System.Data;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class RelationalDatabaseDataReader : IDatabaseDataReader
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataReader(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
        }

        public DataTable GetData(string databaseName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (columnsNames == null)
                throw new ArgumentNullException(nameof(columnsNames));
            
            var dataReader = _database.SendReaderRequest(BuildRequest(databaseName, columnsNames, valuesByWhichSelecting));
            var dataTable = new DataTable();
            
            dataTable.Load(dataReader);
            return dataTable;
        }

        private string BuildRequest(string databaseName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append("SELECT ");
            stringBuilder.Append(columnsNames.Length == 0 ? "*" : _enumerationStringFactory.Create(columnsNames, ", "));
            stringBuilder.Append($" FROM {databaseName}");

            if (valuesByWhichSelecting == null || valuesByWhichSelecting.Length == 0) 
                return stringBuilder.ToString();
            
            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesByWhichSelecting.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            return stringBuilder.ToString();
        }
    }
}