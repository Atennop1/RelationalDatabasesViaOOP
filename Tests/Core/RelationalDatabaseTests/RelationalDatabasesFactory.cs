namespace RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests
{
    public sealed class RelationalDatabasesFactory
    {
        public IDatabase Create()
            => new RelationalDatabase(@"Server=localhost;Port=5434;User Id=postgres;Password=igay123Aa;Database=DatabaseForLibraryTests");
    }
}