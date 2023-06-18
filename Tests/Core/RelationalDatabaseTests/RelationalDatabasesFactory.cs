namespace RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests
{
    public sealed class RelationalDatabasesFactory
    {
        public RelationalDatabase Create()
            => new(@"Server=localhost;Port=5434;User Id=postgres;Password=pleaseDoNotDropMyTables;Database=DatabaseForLibraryTests");
    }
}