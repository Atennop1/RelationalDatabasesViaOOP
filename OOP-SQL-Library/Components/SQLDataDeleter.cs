using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataDeleter : ISQLDataDeleter
    {
        private readonly ISQLCommandsExecutor _sqlCommandsExecutor;
        private readonly ISQLParametersStringBuilder _sqlParametersStringBuilder;

        public SQLDataDeleter(ISQLCommandsExecutor sqlCommandsExecutor, ISQLParametersStringBuilder sqlParametersStringBuilder)
        {
            _sqlParametersStringBuilder = sqlParametersStringBuilder ?? throw new ArgumentNullException(nameof(sqlParametersStringBuilder));
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
        }

        public void DeleteData(string databaseName, ISQLArgument[] sqlArguments)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (sqlArguments == null || sqlArguments.Length == 0)
                throw new ArgumentNullException(nameof(sqlArguments));

            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"DELETE FROM {databaseName} WHERE ");

            finalCommandStringBuilder.Append(_sqlParametersStringBuilder.BuildParameters(sqlArguments.Select(data => $"{data.Name} = {data.Value}").ToArray(), " AND "));
            _sqlCommandsExecutor.ExecuteNonQuery(finalCommandStringBuilder.ToString());
        }
    }
}