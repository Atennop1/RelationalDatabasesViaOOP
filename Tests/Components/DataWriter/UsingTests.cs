using System;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components.DataWriter
{
    public sealed class UsingTests
    {
        private IDatabaseDataWriter _dataWriter;

        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            _dataWriter = new RelationalDatabaseDataWriter(databaseFactory.Create(), new EnumerationStringFactory());
        }
        
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
    }
}