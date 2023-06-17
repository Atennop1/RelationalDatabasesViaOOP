using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataReader
{
    public sealed class UsingTests
    {
        private IDatabase _database;
        private IDatabaseDataReader _dataReader;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _database = databaseFactory.Create();
            _dataReader = new RelationalDatabaseDataReader(_database, new EnumerationStringFactory());
        }
        
        [SetUp]
        public void Setup()
            => _database.SendNonQueryRequest("DELETE FROM humans");

        [Test]
        public void IsUsingCorrect1() 
            => Assert.Throws<ArgumentNullException>(() => _dataReader.Read("humans", null));

        [Test]
        public void IsUsingCorrect2()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var columnNames = new [] { "first_name" };
                _dataReader.Read("", columnNames);
            });
        }
        
        [Test]
        public void IsUsingCorrect3()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var columnNames = new [] { "first_name" };
                _dataReader.Read(null, columnNames);
            });
        }
        
        [Test]
        public void IsUsingCorrect4()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var columnNames = new string[] { };
            
            var dataTable = _dataReader.Read("humans", columnNames);
            Assert.That(dataTable.Rows.Count == 1 && (string)dataTable.Rows[0]["first_name"] == "test");
        }
        
        [Test]
        public void IsUsingCorrect5()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var columnNames = new string[] { };
            var valuesByWhichSelecting = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "not test")};
            
            var dataTable = _dataReader.Read("humans", columnNames, valuesByWhichSelecting);
            Assert.That(dataTable.Rows.Count == 0);
        }
        
        [Test]
        public void IsUsingCorrect6()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var columnNames = new string[] { };
            var valuesByWhichSelecting = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "test")};
            
            var dataTable = _dataReader.Read("humans", columnNames, valuesByWhichSelecting);
            Assert.That(dataTable.Rows.Count == 1 && (string)dataTable.Rows[0]["first_name"] == "test");
        }
        
        [Test]
        public void IsUsingCorrect7()
        {
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");
            var columnNames = new[] { "first_name" };
            
            var dataTable = _dataReader.Read("humans", columnNames, null);
            Assert.That(dataTable.Columns.Count == 1 && (string)dataTable.Rows[0]["first_name"] == "test");
        }
    }
}