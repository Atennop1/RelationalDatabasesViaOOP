using System.Data;
using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataReader : ISQLDataReader
    {
        private readonly ISQLCommandsExecutor _sqlCommandsExecutor;
        private readonly ISQLParametersStringFactory _sqlParametersStringFactory;

        public SQLDataReader(ISQLCommandsExecutor sqlCommandsExecutor, ISQLParametersStringFactory sqlParametersStringFactory)
        {
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
            _sqlParametersStringFactory = sqlParametersStringFactory ?? throw new ArgumentNullException(nameof(sqlParametersStringFactory));
        }

        public DataTable GetData(string databaseName, string[] columnsNames, SQLArgument[] selectingArguments)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (columnsNames == null)
                throw new ArgumentNullException(nameof(columnsNames));

            if (selectingArguments == null)
                throw new ArgumentNullException(nameof(selectingArguments));

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            stringBuilder.Append(columnsNames.Length == 0 ? "*" : _sqlParametersStringFactory.Create(columnsNames, ", "));
            stringBuilder.Append($" FROM {databaseName}");

            if (selectingArguments.Length != 0)
            {
                stringBuilder.Append(" WHERE ");
                stringBuilder.Append(_sqlParametersStringFactory.Create(selectingArguments.Select(argument => $"{argument.Name} = {argument.Value}").ToArray(), " AND "));
            }
            
            var dataReader = _sqlCommandsExecutor.ExecuteReader(stringBuilder.ToString());
            var dataTable = new DataTable();
            
            dataTable.Load(dataReader);
            return dataTable;
        }
    }
}