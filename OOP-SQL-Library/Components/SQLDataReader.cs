using System.Data;

namespace LibrarySQL
{
    public sealed class SQLDataReader
    {
        private readonly SQLCommandsExecutor _sqlCommandsExecutor;

        public SQLDataReader(SQLCommandsExecutor sqlCommandsExecutor) 
            => _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));

        public DataTable GetData(string sqlRequest)
        {
            if (sqlRequest == null)
                throw new ArgumentNullException(nameof(sqlRequest));
            
            var dataReader = _sqlCommandsExecutor.ExecuteReader(sqlRequest);
            var dataTable = new DataTable();
            
            dataTable.Load(dataReader);
            return dataTable;
        }
    }
}