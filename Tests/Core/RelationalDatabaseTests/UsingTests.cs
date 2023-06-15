using System;
using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests
{
    public sealed class UsingTests
    {
        private RelationalDatabase _database;

        [OneTimeSetUp]
        public void OneTimeSetup() 
            => _database = new RelationalDatabasesFactory().Create();

        [SetUp]
        public void Setup()
            => _database.SendNonQueryRequest("DELETE FROM humans");

        [Test]
        public void IsNonQueryRequestCorrect1() 
            => Assert.Throws<ArgumentNullException>(() => _database.SendNonQueryRequest(null!));
        
        [Test]
        public void IsNonQueryRequestCorrect2()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            Assert.That((string)_database.SendReaderRequest("SELECT * FROM humans").Rows[0]["first_name"] == "test");
        }
    }
}