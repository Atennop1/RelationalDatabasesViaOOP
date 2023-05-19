using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataWriter : ISQLDataWriter
    {
        private readonly ISQLCommandsExecutor _sqlCommandsExecutor;
        private readonly ISQLParametersStringFactory _sqlParametersStringFactory;

        public SQLDataWriter(ISQLCommandsExecutor sqlCommandsExecutor, ISQLParametersStringFactory isqlParametersStringFactory)
        {
            _sqlParametersStringFactory = isqlParametersStringFactory ?? throw new ArgumentNullException(nameof(isqlParametersStringFactory));
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
        }

        public void WriteData(string databaseName, ISQLArgument[] sqlArguments)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (sqlArguments == null || sqlArguments.Length == 0)
                throw new ArgumentNullException(nameof(sqlArguments));
            
            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"INSERT INTO {databaseName} (");
            finalCommandStringBuilder.Append(_sqlParametersStringFactory.Create(sqlArguments.Select(argument => argument.Name).ToArray(), ", "));
            finalCommandStringBuilder.Append(")");
            
            finalCommandStringBuilder.Append(" VALUES (");
            finalCommandStringBuilder.Append(_sqlParametersStringFactory.Create(sqlArguments.Select(argument => argument.Value.ToString()).ToArray()!, ", "));
            finalCommandStringBuilder.Append(")");
            
            _sqlCommandsExecutor.ExecuteNonQuery(finalCommandStringBuilder.ToString());
        }
    }
}