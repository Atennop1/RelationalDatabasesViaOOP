using System;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Realisation of <b>IDatabaseDataUpdater</b> interface for relational databases
    /// </summary>
    public sealed class RelationalDatabaseDataUpdater : IDatabaseDataUpdater
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataUpdater(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
        }

        public void Update(string tableName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesByWhichChanging)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (replacedValues == null || replacedValues.Length == 0)
                throw new ArgumentNullException(nameof(replacedValues));
            
            _database.SendNonQueryRequest(BuildRequest(tableName, replacedValues, valuesByWhichChanging));
        }

        private string BuildRequest(string tableName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesByWhichChanging)
        {
            tableName = tableName?.Replace("'", "''");
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append($"UPDATE {tableName} SET ");
            stringBuilder.Append(_enumerationStringFactory.Create(replacedValues.Select(argument => $"{argument.ColumnName} = {argument.Get()}").ToArray(), " AND "));

            if (valuesByWhichChanging == null || valuesByWhichChanging.Length == 0)
                return stringBuilder.ToString();

            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesByWhichChanging.Select(argument => $"{argument.ColumnName} = {argument.Get()}").ToArray(), " AND "));
            return stringBuilder.ToString();
        }
    }
}