using System.Reflection;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Components
{
    public sealed class DataDeleterTests
    {
        private IDatabaseDataDeleter _databaseDataDeleter;
        private MethodInfo _buildRequestMethodInfo;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            var enumerationStringFactory = new EnumerationStringFactory();
            
            _databaseDataDeleter = new RelationalDatabaseDataDeleter(databaseFactory.Create(),enumerationStringFactory);
            _buildRequestMethodInfo = _databaseDataDeleter.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)[0];
        }

        [Test]
        public void IsBuildRequestCorrect1()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataDeleter, new object?[] 
            { 
                "humans", 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "DELETE FROM humans WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
        [Test]
        public void IsBuildRequestCorrect2()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataDeleter, new object?[] { "humans", new IDatabaseValue[] { } })!;
            Assert.That(result == "DELETE FROM humans WHERE ");
        }
        
        [Test]
        public void IsBuildRequestCorrect3()
        {
            Assert.Throws<TargetInvocationException>(() =>
            {
                _buildRequestMethodInfo.Invoke(_databaseDataDeleter, new object?[] { "humans", null });
            }); 
        }
        
        [Test]
        public void IsBuildRequestCorrect4()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataDeleter, new object?[] { null, new IDatabaseValue[] { } })!;
            Assert.That(result == "DELETE FROM  WHERE ");
        }
    }
}