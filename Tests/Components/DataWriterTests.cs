using System.Reflection;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Runtime;
using RelationalDatabasesViaOOP.Tests.Tests.Core.RelationalDatabaseTests;

namespace RelationalDatabasesViaOOP.Tests.Tests.Components
{
    public sealed class DataWriterTests
    {
        private IDatabaseDataWriter _databaseDataWriter;
        private MethodInfo _buildRequestMethodInfo;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            var enumerationStringFactory = new EnumerationStringFactory();
            
            _databaseDataWriter = new RelationalDatabaseDataWriter(databaseFactory.Create(),enumerationStringFactory);
            _buildRequestMethodInfo = _databaseDataWriter.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)[0];
        }

        [Test]
        public void IsBuildRequestCorrect1()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataWriter, new object[] 
            { 
                "humans", 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "INSERT INTO humans (first_name, last_name) VALUES ('grigoriy', 'fedotkin')");
        }
        
        [Test]
        public void IsBuildRequestCorrect2()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataWriter, new object[] { "humans", new IDatabaseValue[] { } })!;
            Assert.That(result == "INSERT INTO humans () VALUES ()");
        }
        
        [Test]
        public void IsBuildRequestCorrect3()
        {
            Assert.Throws<TargetInvocationException>(() =>
            {
                _buildRequestMethodInfo.Invoke(_databaseDataWriter, new object[] { "humans", null });
            }); 
        }
        
        [Test]
        public void IsBuildRequestCorrect4()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataWriter, new object[] { null, new IDatabaseValue[] { } })!;
            Assert.That(result == "INSERT INTO  () VALUES ()");
        }
    }
}