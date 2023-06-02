using System;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class RelationalDatabaseDataUpdater : IDatabaseDataUpdater
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataUpdater(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
        }

        public void UpdateData(string databaseName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesWhichChanging = null!)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (replacedValues == null || replacedValues.Length == 0)
                throw new ArgumentNullException(nameof(replacedValues));

            _database.SendNonQueryRequest(BuildRequest(databaseName, replacedValues, valuesWhichChanging));
        }

        private string BuildRequest(string databaseName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesWhichChanging)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {databaseName} SET ");
            stringBuilder.Append(_enumerationStringFactory.Create(replacedValues.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));

            if (valuesWhichChanging == null || valuesWhichChanging.Length == 0)
                return stringBuilder.ToString();

            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesWhichChanging.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            return stringBuilder.ToString();
        }
    }
}