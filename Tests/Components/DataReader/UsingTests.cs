using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataReader
{
    public sealed class UsingTests
    {
        private IDatabaseDataReader _dataReader;

        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _dataReader = new RelationalDatabaseDataReader(databaseFactory.Create(), new EnumerationStringFactory());
        }

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
            var columnNames = new string[] { };
            _dataReader.Read("humans", columnNames);
        }
        
        [Test]
        public void IsUsingCorrect5()
        {
            var columnNames = new string[] { };
            var valuesByWhichSelecting = new IDatabaseValue[] { };
            _dataReader.Read("humans", columnNames, valuesByWhichSelecting);
        }
        
        [Test]
        public void IsUsingCorrect6()
        {
            var columnNames = new string[] { };
            _dataReader.Read("humans", columnNames, null);
        }
    }
}