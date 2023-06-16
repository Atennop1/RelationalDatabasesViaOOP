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
            Assert.That((string)_database.SendReadingRequest("SELECT * FROM humans").Rows[0]["first_name"] == "test");
        }
        
        [Test]
        public void IsReadingRequestCorrect1() 
            => Assert.Throws<ArgumentNullException>(() => _database.SendReadingRequest(null!));
        
        [Test]
        public void IsReadingRequestCorrect2()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var data = _database.SendReadingRequest("SELECT * FROM humans");
            Assert.That((string)data.Rows[0]["first_name"] == "test");
        }
        
        [Test]
        public void IsScalarRequestCorrect1() 
            => Assert.Throws<ArgumentNullException>(() => _database.SendScalarRequest(null!));
        
        [Test]
        public void IsScalarRequestCorrect2()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (age) VALUES (13)");
            var data = _database.SendScalarRequest("SELECT * FROM humans");
            Assert.That(data != null && (int)data == 13);
        }
    }
}