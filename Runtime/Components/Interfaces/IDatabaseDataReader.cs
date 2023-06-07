using System.Data;

namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Interface for object which will be read any data from database
    /// </summary>
    public interface IDatabaseDataReader
    {
        /// <summary>
        /// Method for reading data from database
        /// </summary>
        /// <param name="tableName">
        /// The name of the table from which you will be deleting data<br/>
        /// For example: <b>"humans"</b>
        /// </param>
        /// <param name="columnsNames">
        /// The names of columns which will be returned<br/>
        /// For example: <b>{ "first_name", "last_name" }</b>
        /// </param>
        /// <param name="valuesByWhichSelecting">
        /// Optional parameter which is the value by which the data will be received<br/>
        /// For example: <b>{ new RelationalDatabaseValue("age", 19) }</b>
        /// </param>
        /// <returns>
        /// DataTable with read data<br/>
        /// Final request from examples below will be this:<br/><b>"SELECT first_name, last_name FROM humans WHERE age = 19"</b>
        /// </returns>
        DataTable Read(string tableName, string[] columnsNames, IDatabaseValue[] valuesByWhichSelecting = null!);
    }
}