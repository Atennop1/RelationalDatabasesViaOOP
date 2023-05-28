using System;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class RelationalDatabaseDataUpdater : IDatabaseDataUpdater
    {
        private readonly IDatabase _database;
        private readonly IDatabaseParametersStringFactory _databaseParametersStringFactory;

        public RelationalDatabaseDataUpdater(IDatabase database, IDatabaseParametersStringFactory isqlParametersStringFactory)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _databaseParametersStringFactory = isqlParametersStringFactory ?? throw new ArgumentNullException(nameof(isqlParametersStringFactory));
        }

        public void UpdateData(string databaseName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesWhichChanging = null!)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (replacedValues == null || replacedValues.Length == 0)
                throw new ArgumentNullException(nameof(replacedValues));

            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"UPDATE {databaseName} SET ");
            finalCommandStringBuilder.Append(_databaseParametersStringFactory.Create(replacedValues.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));

            if (valuesWhichChanging != null && replacedValues.Length != 0)
            {
                finalCommandStringBuilder.Append(" WHERE ");
                finalCommandStringBuilder.Append(_databaseParametersStringFactory.Create(valuesWhichChanging.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            }
            
            _database.SendNonQueryRequest(finalCommandStringBuilder.ToString());
        }
    }
}