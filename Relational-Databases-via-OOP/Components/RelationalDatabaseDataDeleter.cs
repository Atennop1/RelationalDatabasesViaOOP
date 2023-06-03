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

        public void DeleteData(string tableName, IDatabaseValue[] valuesByWhichDeleting)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (valuesByWhichDeleting == null || valuesByWhichDeleting.Length == 0)
                throw new ArgumentNullException(nameof(valuesByWhichDeleting));
            
            _database.SendNonQueryRequest(BuildRequest(tableName, valuesByWhichDeleting));
        }

        private string BuildRequest(string tableName, IDatabaseValue[] valuesByWhichDeleting)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"DELETE FROM {tableName} WHERE ");

            stringBuilder.Append(_enumerationStringFactory.Create(valuesByWhichDeleting.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            return stringBuilder.ToString();
        }
    }
}