using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataDeleter
{
    public sealed class UsingTests
    {
        private IDatabase _database;
        private IDatabaseDataDeleter _dataDeleter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _database = databaseFactory.Create();
            _dataDeleter = new RelationalDatabaseDataDeleter(_database, new EnumerationStringFactory());
        }

        [SetUp]
        public void Setup()
            => _database.SendNonQueryRequest("DELETE FROM humans");
        
        [Test]
        public void IsUsingCorrect1()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesByWhichDeleting = new IDatabaseValue[] { };
                _dataDeleter.Delete("humans", valuesByWhichDeleting);
            });
        }
        
        [Test]
        public void IsUsingCorrect2() 
            => Assert.Throws<ArgumentNullException>(() => _dataDeleter.Delete("humans", null));

        [Test]
        public void IsUsingCorrect3()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesByWhichDeleting = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataDeleter.Delete("", valuesByWhichDeleting);
            });
        }
        
        [Test]
        public void IsUsingCorrect4()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesByWhichDeleting = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataDeleter.Delete(null, valuesByWhichDeleting);
            });
        }

        [Test]
        public void IsUsingCorrect5()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var tableBeforeDeleting = _database.SendReadingRequest("SELECT * FROM humans");
            
            _dataDeleter.Delete("humans", new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "test") });
            var tableAfterDeleting = _database.SendReadingRequest("SELECT * FROM humans");
            
            Assert.That(tableBeforeDeleting.Rows.Count == 1 && (string)tableBeforeDeleting.Rows[0]["first_name"] == "test" && tableAfterDeleting.Rows.Count == 0);
        }
    }
}