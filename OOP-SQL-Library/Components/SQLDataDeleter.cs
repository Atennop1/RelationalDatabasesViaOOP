using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataDeleter : ISQLDataDeleter
    {
        private readonly ISQLCommandsExecutor _sqlCommandsExecutor;
        private readonly ISQLParametersStringFactory _sqlParametersStringFactory;

        public SQLDataDeleter(ISQLCommandsExecutor sqlCommandsExecutor, ISQLParametersStringFactory isqlParametersStringFactory)
        {
            _sqlParametersStringFactory = isqlParametersStringFactory ?? throw new ArgumentNullException(nameof(isqlParametersStringFactory));
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
        }

        public void DeleteData(string databaseName, ISQLArgument[] argumentByWhichDeleting)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (argumentByWhichDeleting == null || argumentByWhichDeleting.Length == 0)
                throw new ArgumentNullException(nameof(argumentByWhichDeleting));

            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"DELETE FROM {databaseName} WHERE ");

            finalCommandStringBuilder.Append(_sqlParametersStringFactory.Create(argumentByWhichDeleting.Select(argument => $"{argument.Name} = {argument.Value}").ToArray(), " AND "));
            _sqlCommandsExecutor.ExecuteNonQuery(finalCommandStringBuilder.ToString());
        }
    }
}