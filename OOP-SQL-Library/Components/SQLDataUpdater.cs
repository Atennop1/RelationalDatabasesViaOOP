using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataUpdater
    {
        private readonly SQLCommandsExecutor _sqlCommandsExecutor;
        private readonly SQLParametersStringBuilder _sqlParametersStringBuilder;

        public SQLDataUpdater(SQLCommandsExecutor sqlCommandsExecutor, SQLParametersStringBuilder sqlParametersStringBuilder)
        {
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
            _sqlParametersStringBuilder = sqlParametersStringBuilder ?? throw new ArgumentNullException(nameof(sqlParametersStringBuilder));
        }

        public void UpdateData(string databaseName, SQLArgument[] argumentsThatChanging, SQLArgument[] argumentsForWhichChanging)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (argumentsThatChanging == null || argumentsThatChanging.Length == 0)
                throw new ArgumentNullException(nameof(argumentsThatChanging));
            
            if (argumentsForWhichChanging == null || argumentsForWhichChanging.Length == 0)
                throw new ArgumentNullException(nameof(argumentsForWhichChanging));
            
            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"UPDATE {databaseName} SET ");
            finalCommandStringBuilder.Append(_sqlParametersStringBuilder.BuildParameters(argumentsThatChanging.Select(data => $"{data.Name} = {data.Value}").ToArray(), " AND "));

            if (argumentsThatChanging.Length != 0)
            {
                finalCommandStringBuilder.Append(" WHERE ");
                finalCommandStringBuilder.Append(_sqlParametersStringBuilder.BuildParameters(argumentsForWhichChanging.Select(data => $"{data.Name} = {data.Value}").ToArray(), " AND "));
            }
            
            _sqlCommandsExecutor.ExecuteNonQuery(finalCommandStringBuilder.ToString());
        }
    }
}