using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataUpdater : ISQLDataUpdater
    {
        private readonly ISQLCommandsExecutor _sqlCommandsExecutor;
        private readonly ISQLParametersStringFactory _sqlParametersStringFactory;

        public SQLDataUpdater(ISQLCommandsExecutor sqlCommandsExecutor, ISQLParametersStringFactory isqlParametersStringFactory)
        {
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
            _sqlParametersStringFactory = isqlParametersStringFactory ?? throw new ArgumentNullException(nameof(isqlParametersStringFactory));
        }

        public void UpdateData(string databaseName, ISQLArgument[] argumentsThatChanging, ISQLArgument[] argumentsForWhichChanging)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (argumentsThatChanging == null || argumentsThatChanging.Length == 0)
                throw new ArgumentNullException(nameof(argumentsThatChanging));
            
            if (argumentsForWhichChanging == null || argumentsForWhichChanging.Length == 0)
                throw new ArgumentNullException(nameof(argumentsForWhichChanging));
            
            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"UPDATE {databaseName} SET ");
            finalCommandStringBuilder.Append(_sqlParametersStringFactory.Create(argumentsThatChanging.Select(argument => $"{argument.Name} = {argument.Value}").ToArray(), " AND "));

            if (argumentsThatChanging.Length != 0)
            {
                finalCommandStringBuilder.Append(" WHERE ");
                finalCommandStringBuilder.Append(_sqlParametersStringFactory.Create(argumentsForWhichChanging.Select(argument => $"{argument.Name} = {argument.Value}").ToArray(), " AND "));
            }
            
            _sqlCommandsExecutor.ExecuteNonQuery(finalCommandStringBuilder.ToString());
        }
    }
}