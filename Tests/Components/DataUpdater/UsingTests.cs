using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataUpdater
{
    public sealed class UsingTests
    {
        private IDatabaseDataUpdater _dataUpdater;

        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _dataUpdater = new RelationalDatabaseDataUpdater(databaseFactory.Create(), new EnumerationStringFactory());
        }

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
            var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("last_name", 10) };
            var valuesByWhichChanging = new IDatabaseValue[] { };
            _dataUpdater.Update("humans", replacedValues, valuesByWhichChanging);
        }
        
        [Test]
        public void IsUsingCorrect6()
        {
            var replacedValues = new IDatabaseValue[] { new RelationalDatabaseValue("last_name", 10) };
            _dataUpdater.Update("humans", replacedValues, null);
        }
    }
}