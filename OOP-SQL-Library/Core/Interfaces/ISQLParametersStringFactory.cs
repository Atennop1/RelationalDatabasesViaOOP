namespace LibrarySQL
{
    public interface ISQLParametersStringFactory
    {
        string Create(string[] names, string delimiter);
    }
}