using System;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP
{
    public sealed class RelationalDatabaseDataWriter : IDatabaseDataWriter
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataWriter(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public void WriteData(string databaseName, IDatabaseValue[] valuesWhichWriting)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (valuesWhichWriting == null || valuesWhichWriting.Length == 0)
                throw new ArgumentNullException(nameof(valuesWhichWriting));
            
            _database.SendNonQueryRequest(BuildRequest(databaseName, valuesWhichWriting));
        }

        private string BuildRequest(string databaseName, IDatabaseValue[] valuesWhichWriting)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append($"INSERT INTO {databaseName} (");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesWhichWriting.Select(argument => argument.Name).ToArray(), ", "));
            stringBuilder.Append(")");
            
            stringBuilder.Append(" VALUES (");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesWhichWriting.Select(argument => argument.Get().ToString()).ToArray()!, ", "));
            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}