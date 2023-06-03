using System.Reflection;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components
{
    public sealed class DataReaderTests
    {
        private IDatabaseDataReader _databaseDataReader;
        private MethodInfo _buildRequestMethodInfo;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            var enumerationStringFactory = new EnumerationStringFactory();
            
            _databaseDataReader = new RelationalDatabaseDataReader(databaseFactory.Create(),enumerationStringFactory);
            _buildRequestMethodInfo = _databaseDataReader.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)[0];
        }
        
        [Test]
        public void IsBuildRequestCorrect1()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataReader, new object?[] 
            { 
                "humans", 
                new [] { "first_name", "last_name" },
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "SELECT first_name, last_name FROM humans WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
        [Test]
        public void IsBuildRequestCorrect2()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataReader, new object?[] 
            { 
                null, 
                new [] { "first_name", "last_name" },
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "SELECT first_name, last_name FROM  WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
        [Test]
        public void IsBuildRequestCorrect3()
        {
            Assert.Throws<TargetInvocationException>(() =>
            {
                _buildRequestMethodInfo.Invoke(_databaseDataReader, new object?[]
                {
                    "humans",
                    null,
                    new IDatabaseValue[]
                    {
                        new RelationalDatabaseValue("first_name", "grigoriy"),
                        new RelationalDatabaseValue("last_name", "fedotkin")
                    }
                });
            });
        }
        
        [Test]
        public void IsBuildRequestCorrect4()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataReader, new object?[] 
            { 
                "humans", 
                new string[] { },
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "SELECT * FROM humans WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
                
        [Test]
        public void IsBuildRequestCorrect5()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataReader, new object?[] 
            { 
                "humans", 
                new [] { "first_name", "last_name" },
                null 
            })!;
            
            Assert.That(result == "SELECT first_name, last_name FROM humans");
        }
        
        [Test]
        public void IsBuildRequestCorrect6()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataReader, new object?[] 
            { 
                "humans", 
                new [] { "first_name", "last_name" },
                new RelationalDatabaseValue[] { } 
            })!;
            
            Assert.That(result == "SELECT first_name, last_name FROM humans");
        }
    }
}