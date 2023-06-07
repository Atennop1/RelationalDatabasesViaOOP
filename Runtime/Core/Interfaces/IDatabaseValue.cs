namespace RelationalDatabasesViaOOP
{
    /// <summary>
    /// Interface for any value in any database
    /// </summary>
    public interface IDatabaseValue
    {
        /// <summary>
        /// The name of the column in which the value is located
        /// </summary>
        string ColumnName { get; }
        
        /// <summary>
        /// Method for getting value from database
        /// </summary>
        /// <returns>
        /// Value containing in database
        /// </returns>
        object Get();
    }
}