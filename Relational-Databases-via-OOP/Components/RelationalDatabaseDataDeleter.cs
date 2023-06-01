using System;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class RelationalDatabaseDataDeleter : IDatabaseDataDeleter
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataDeleter(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public void DeleteData(string databaseName, IDatabaseValue[] valuesByWhichDeleting)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (valuesByWhichDeleting == null || valuesByWhichDeleting.Length == 0)
                throw new ArgumentNullException(nameof(valuesByWhichDeleting));
            
            _database.SendNonQueryRequest(BuildRequest(databaseName, valuesByWhichDeleting));
        }

        private string BuildRequest(string databaseName, IDatabaseValue[] valuesByWhichDeleting)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"DELETE FROM {databaseName} WHERE ");

            stringBuilder.Append(_enumerationStringFactory.Create(valuesByWhichDeleting.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            return stringBuilder.ToString();
        }
    }
}