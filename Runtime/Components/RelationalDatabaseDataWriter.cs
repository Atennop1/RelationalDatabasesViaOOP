using System;
using System.Linq;
using System.Text;

namespace RelationalDatabasesViaOOP.Runtime
{
    /// <summary>
    /// Realisation of <b>IDatabaseDataWriter</b> interface for relational databases
    /// </summary>
    public sealed class RelationalDatabaseDataWriter : IDatabaseDataWriter
    {
        private readonly IDatabase _database;
        private readonly IEnumerationStringFactory _enumerationStringFactory;

        public RelationalDatabaseDataWriter(IDatabase database, IEnumerationStringFactory enumerationStringFactory)
        {
            _enumerationStringFactory = enumerationStringFactory ?? throw new ArgumentNullException(nameof(enumerationStringFactory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public void Write(string tableName, IDatabaseValue[] valuesWhichWriting)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (valuesWhichWriting == null || valuesWhichWriting.Length == 0)
                throw new ArgumentNullException(nameof(valuesWhichWriting));
            
            _database.SendNonQueryRequest(BuildRequest(tableName, valuesWhichWriting));
        }

        private string BuildRequest(string tableName, IDatabaseValue[] valuesWhichWriting)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append($"INSERT INTO {tableName} (");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesWhichWriting.Select(argument => argument.ColumnName).ToArray(), ", "));
            stringBuilder.Append(")");
            
            stringBuilder.Append(" VALUES (");
            stringBuilder.Append(_enumerationStringFactory.Create(valuesWhichWriting.Select(argument => argument.Get().ToString()).ToArray()!, ", "));
            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}