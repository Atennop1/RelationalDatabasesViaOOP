namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Interface for object which will writing data into database
    /// </summary>
    public interface IDatabaseDataWriter
    {
        /// <summary>
        /// Method for writing data into database<br/>
        /// In the example below, the final query would be this:<br/><b>"INSERT INTO humans (first_name) VALUES ('anton')"</b>
        /// </summary>
        /// <param name="tableName">
        /// The name of the table from which you will be deleting data<br/>
        /// For example: <b>"humans"</b>
        /// </param>
        /// <param name="valuesWhichWriting">
        /// Values to be inserted into the database<br/>
        /// For example: <b>{ new RelationalDatabaseValue("first_name", "anton") }</b>
        /// </param>
        void Write(string tableName, IDatabaseValue[] valuesWhichWriting);
    }
}