using System;
using System.Data;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Realisation of <b>IDatabaseDataReader</b> interface for relational databases
    /// </summary>
    public sealed class RelationalDatabaseDataReader : IDatabaseDataReader
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataReader(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
        }

        public DataTable Read(string tableName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (columnsNames == null)
                throw new ArgumentNullException(nameof(columnsNames));
            
            return _database.SendReadingRequest(BuildRequest(tableName, columnsNames, valuesByWhichSelecting));
        }

        private string BuildRequest(string tableName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append("SELECT ");
            stringBuilder.Append(columnsNames.Length == 0 ? "*" : _enumerationStringFactory.Create(columnsNames, ", "));
            stringBuilder.Append($" FROM {tableName}");

            if (valuesByWhichSelecting == null || valuesByWhichSelecting.Length == 0) 
                return stringBuilder.ToString();
            
            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesByWhichSelecting.Select(argument => $"{argument.ColumnName} = {argument.Get()}").ToArray(), " AND "));
            return stringBuilder.ToString();
        }
    }
}