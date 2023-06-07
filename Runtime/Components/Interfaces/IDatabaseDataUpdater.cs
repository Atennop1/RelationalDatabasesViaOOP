namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Interface for object which will be updating data in database
    /// </summary>
    public interface IDatabaseDataUpdater
    {
        /// <summary>
        /// Method for updating data in database<br/>
        /// In the example below, the final query would be this:<br/><b>"UPDATE humans SET age = 19 WHERE age = 15"</b>
        /// </summary>
        /// <param name="tableName">
        /// The name of the table from which you will be deleting data<br/>
        /// For example: <b>"humans"</b>
        /// </param>
        /// <param name="replacedValues">
        /// Values that will replace the original<br/>
        /// For example: <b>{ new RelationalDatabaseValue("age", 19) }</b>
        /// </param>
        /// <param name="valuesWhichChanging">
        /// Values to be replaced<br/>
        /// For example: <b>{ new RelationalDatabaseValue("age", 15) }</b>
        /// </param>
        void Update(string tableName, IDatabaseValue[] replacedValues, IDatabaseValue[] valuesWhichChanging);
    }
}