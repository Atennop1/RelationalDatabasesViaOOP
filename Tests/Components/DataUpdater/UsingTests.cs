using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataUpdater
{
    public sealed class UsingTests
    {
        private IDatabase _database;
        private IDatabaseDataUpdater _dataUpdater;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _database = databaseFactory.Create();
            _dataUpdater = new RelationalDatabaseDataUpdater(_database, new EnumerationStringFactory());
        }
        
        [SetUp]
        public void Setup()
            => _database.SendNonQueryRequest("DELETE FROM humans");

        [Test]
        public void IsUsingCorrect1()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesByWhichChanging = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataUpdater.Update("humans", null, valuesByWhichChanging);
            });
        }
        
        [Test]
        public void IsUsingCorrect2()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var replacedValues = new IDatabaseValue[] { };
                var valuesByWhichChanging = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataUpdater.Update("humans", replacedValues, valuesByWhichChanging);
            });
        }

        [Test]
        public void IsUsingCorrect3()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("last_name", 10) };
                var valuesByWhichChanging = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataUpdater.Update("", replacedValues, valuesByWhichChanging);
            });
        }
        
        [Test]
        public void IsUsingCorrect4()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("last_name", 10) };
                var valuesByWhichChanging = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataUpdater.Update(null, replacedValues, valuesByWhichChanging);
            });
        }
        
        [Test]
        public void IsUsingCorrect5()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var tableBeforeUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            
            var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "not test") };
            _dataUpdater.Update("humans", replacedValues, new IDatabaseValue[] { });
            
            var tableAfterUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            Assert.That(tableBeforeUpdating.Rows.Count == 1 && (string)tableBeforeUpdating.Rows[0]["first_name"] == "test" &&
                        tableAfterUpdating.Rows.Count == 1 && (string)tableAfterUpdating.Rows[0]["first_name"] == "not test");
        }
        
        [Test]
        public void IsUsingCorrect6()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var tableBeforeUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            
            var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "not test") };
            _dataUpdater.Update("humans", replacedValues, null);
            
            var tableAfterUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            Assert.That(tableBeforeUpdating.Rows.Count == 1 && (string)tableBeforeUpdating.Rows[0]["first_name"] == "test" &&
                        tableAfterUpdating.Rows.Count == 1 && (string)tableAfterUpdating.Rows[0]["first_name"] == "not test");
        }
        
        [Test]
        public void IsUsingCorrect7()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var tableBeforeUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            
            var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "not test") };
            _dataUpdater.Update("humans", replacedValues, new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "idk") });
            
            var tableAfterUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            Assert.That(tableBeforeUpdating.Rows.Count == 1 && (string)tableBeforeUpdating.Rows[0]["first_name"] == "test" &&
                        tableAfterUpdating.Rows.Count == 1 && (string)tableAfterUpdating.Rows[0]["first_name"] == "test");
        }
        
        [Test]
        public void IsUsingCorrect8()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var tableBeforeUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            
            var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "not test") };
            _dataUpdater.Update("humans", replacedValues, new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "test") });
            
            var tableAfterUpdating = _database.SendReadingRequest("SELECT * FROM humans");
            Assert.That(tableBeforeUpdating.Rows.Count == 1 && (string)tableBeforeUpdating.Rows[0]["first_name"] == "test" &&
                        tableAfterUpdating.Rows.Count == 1 && (string)tableAfterUpdating.Rows[0]["first_name"] == "not test");
        }
    }
}