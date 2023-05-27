using System.Text;

namespace LibrarySQL
{
    public sealed class RelationalDatabaseDataWriter : IDatabaseDataWriter
    {
        private readonly IDatabase _database;
        private readonly IDatabaseParametersStringFactory _databaseParametersStringFactory;

        public RelationalDatabaseDataWriter(IDatabase database, IDatabaseParametersStringFactory isqlParametersStringFactory)
        {
            _databaseParametersStringFactory = isqlParametersStringFactory ?? throw new ArgumentNullException(nameof(isqlParametersStringFactory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public void WriteData(string databaseName, IDatabaseValue[] valuesWhichWriting)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (valuesWhichWriting == null || valuesWhichWriting.Length == 0)
                throw new ArgumentNullException(nameof(valuesWhichWriting));
            
            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"INSERT INTO {databaseName} (");
            finalCommandStringBuilder.Append(_databaseParametersStringFactory.Create(valuesWhichWriting.Select(argument => argument.Name).ToArray(), ", "));
            finalCommandStringBuilder.Append(")");
            
            finalCommandStringBuilder.Append(" VALUES (");
            finalCommandStringBuilder.Append(_databaseParametersStringFactory.Create(valuesWhichWriting.Select(argument => argument.Get().ToString()).ToArray()!, ", "));
            finalCommandStringBuilder.Append(")");
            
            _database.ExecuteNonQueryCommand(finalCommandStringBuilder.ToString());
        }
    }
}