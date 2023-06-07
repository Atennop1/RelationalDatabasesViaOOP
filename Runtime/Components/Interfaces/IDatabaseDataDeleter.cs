namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Interface for object which will be deleting data from database
    /// </summary>
    public interface IDatabaseDataDeleter
    {
        /// <summary>
        /// Method for deleting data from database<br/>
        /// In the example below, the final query would be this:<br/><b>"DELETE FROM humans WHERE first_name = 'anton'"</b>
        /// </summary>
        /// <param name="tableName">
        /// The name of the table from which you will be deleting data<br/>
        /// For example: <b>"humans"</b>
        /// </param>
        /// <param name="valuesByWhichDeleting">
        /// Values by which data will be deleted<br/>
        /// For example: <b>{ new RelationalDatabaseValue("first_name", "anton") }</b>
        /// </param>
        void Delete(string tableName, IDatabaseValue[] valuesByWhichDeleting);
    }
}