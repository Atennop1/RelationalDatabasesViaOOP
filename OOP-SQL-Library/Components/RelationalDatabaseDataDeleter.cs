using System.Text;

namespace LibrarySQL
{
    public sealed class RelationalDatabaseDataDeleter : IDatabaseDataDeleter
    {
        private readonly IDatabase _database;
        private readonly IDatabaseParametersStringFactory _databaseParametersStringFactory;

        public RelationalDatabaseDataDeleter(IDatabase database, IDatabaseParametersStringFactory isqlParametersStringFactory)
        {
            _databaseParametersStringFactory = isqlParametersStringFactory ?? throw new ArgumentNullException(nameof(isqlParametersStringFactory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public void DeleteData(string databaseName, IDatabaseValue[] valuesByWhichDeleting)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (valuesByWhichDeleting == null || valuesByWhichDeleting.Length == 0)
                throw new ArgumentNullException(nameof(valuesByWhichDeleting));

            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"DELETE FROM {databaseName} WHERE ");

            finalCommandStringBuilder.Append(_databaseParametersStringFactory.Create(valuesByWhichDeleting.Select(argument => $"{argument.Name} = {argument.Get()}").ToArray(), " AND "));
            _database.ExecuteNonQueryCommand(finalCommandStringBuilder.ToString());
        }
    }
}