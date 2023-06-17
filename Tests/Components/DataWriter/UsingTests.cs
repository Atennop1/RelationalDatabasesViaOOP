using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataWriter
{
    public sealed class UsingTests
    {
        private IDatabase _database;
        private IDatabaseDataWriter _dataWriter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _database = databaseFactory.Create();
            _dataWriter = new RelationalDatabaseDataWriter(_database, new EnumerationStringFactory());
        }
        
        [SetUp]
        public void Setup()
            => _database.SendNonQueryRequest("DELETE FROM humans");
        
        [Test]
        public void IsUsingCorrect1()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesWhichWriting = new IDatabaseValue[] { };
                _dataWriter.Write("humans", valuesWhichWriting);
            });
        }
        
        [Test]
        public void IsUsingCorrect2() 
            => Assert.Throws<ArgumentNullException>(() => _dataWriter.Write("humans", null));

        [Test]
        public void IsUsingCorrect3()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesWhichWriting = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataWriter.Write("", valuesWhichWriting);
            });
        }
        
        [Test]
        public void IsUsingCorrect4()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var valuesWhichWriting = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", 10) };
                _dataWriter.Write(null, valuesWhichWriting);
            });
        }
        
        [Test]
        public void IsUsingCorrect5()
        {
            var tableBeforeWriting = _database.SendReadingRequest("SELECT * FROM humans");
            _database.SendNonQueryRequest("INSERT INTO humans (first_name) VALUES ('test')");

            var tableAfterWriting = _database.SendReadingRequest("SELECT * FROM humans");
            Assert.That(tableBeforeWriting.Rows.Count == 0 && tableAfterWriting.Rows.Count == 1 && (string)tableAfterWriting.Rows[0]["first_name"] == "test");
        }
    }
}