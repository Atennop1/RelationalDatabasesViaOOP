namespace RelationalDatabasesViaOOP.Runtime
{
    /// <summary>
    /// Interface for factory to create enum strings
    /// </summary>
    public interface IEnumerationStringFactory
    {
        /// <summary>
        /// Method for creating string with enumeration<br/>For example: <b>"anton, anatoliy, ekaterina"</b>
        /// </summary>
        /// <param name="strings">
        /// The strings that will be in the enum<br/>In the example below, this argument looks like this: <b>{ "anton", "anatoliy", "ekaterina" }</b>
        /// </param>
        /// <param name="delimiter">
        /// Delimiter of the strings<br/>In example below delimiter is: <b>", "</b>
        /// </param>
        /// <returns>
        /// The final string with enumeration
        /// </returns>
        string Create(string[] strings, string delimiter);
    }
}