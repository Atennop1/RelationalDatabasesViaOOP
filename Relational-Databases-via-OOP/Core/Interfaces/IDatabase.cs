#nullable enable
using System.Data;
using System.Threading.Tasks;

namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// An interface for working with databases that allows you to execute various types of queries synchronously and asynchronously
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Method for executing a query that returns some kind of data reader that can be unloaded into a DataTable and then use the received data
        /// </summary>
        /// <param name="commandText">
        /// Database query text
        /// </param>
        /// <returns>
        /// DataReader with query result which you can unload to DataTable
        /// </returns>
        IDataReader SendReaderRequest(string commandText);
        
        /// <summary>
        /// A method for executing a request that does not return any value
        /// </summary>
        /// <param name="commandText">
        /// Database query text
        /// </param>
        /// <returns>
        /// The number of affected rows
        /// </returns>
        int SendNonQueryRequest(string commandText);
        
        /// <summary>
        /// Method for executing queries that return zero or one row of one column
        /// </summary>
        /// <param name="commandText">
        /// Database query text
        /// </param>
        /// <returns>
        /// One row of one column or null
        /// </returns>
        object? SendScalarRequest(string commandText);
    
        /// <summary>
        /// Async version of SendReaderRequest used for executing a query that returns some kind of data reader that can be unloaded into a DataTable and then use the received data
        /// </summary>
        /// <param name="commandText">
        /// Database query text
        /// </param>
        /// <returns>
        /// DataReader with query result which you can unload to DataTable
        /// </returns>
        Task<IDataReader> SendReaderRequestAsync(string commandText);
        
        /// <summary>
        /// Async version of SendNonQuery request used for executing a request that does not return any value
        /// </summary>
        /// <param name="commandText">
        /// Database query text
        /// </param>
        /// <returns>
        /// The number of affected rows
        /// </returns>
        Task<int> SendNonQueryRequestAsync(string commandText);
        
        /// <summary>
        /// Async version of SendScalarRequest used for executing queries that return zero or one row of one column
        /// </summary>
        /// <param name="commandText">
        /// Database query text
        /// </param>
        /// <returns>
        /// One row of one column or null
        /// </returns>
        Task<object?> SendScalarRequestAsync(string commandText);
    }
}