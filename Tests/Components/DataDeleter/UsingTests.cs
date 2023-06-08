using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataDeleter
{
    public sealed class UsingTests
    {
        private IDatabaseDataDeleter _dataDeleter;

        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _dataDeleter = new RelationalDatabaseDataDeleter(databaseFactory.Create(), new EnumerationStringFactory());
        }
        
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
    }
}