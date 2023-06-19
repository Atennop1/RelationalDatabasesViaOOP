#nullable enable
using System.Reflection;
using NUnit.Framework;
using RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests;

#pragma warning disable CS8602

namespace RelationalDatabasesViaOOP.Tests.Components.DataUpdater
{
    public sealed class RequestBuildingTests
    {
        private IDatabaseDataUpdater? _databaseDataUpdater;
        private MethodInfo? _buildRequestMethodInfo;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var databaseFactory = new RelationalDatabasesFactory();
            var enumerationStringFactory = new EnumerationStringFactory();
            
            _databaseDataUpdater = new RelationalDatabaseDataUpdater(databaseFactory.Create(),enumerationStringFactory);
            _buildRequestMethodInfo = _databaseDataUpdater.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)[0];
        }
        
        [Test]
        public void IsBuildRequestCorrect1()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[] 
            { 
                "humans", 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "anatoliy"),
                    new RelationalDatabaseValue("last_name", "oleynikov")
                },
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "UPDATE humans SET first_name = 'anatoliy' AND last_name = 'oleynikov' WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
        [Test]
        public void IsBuildRequestCorrect2()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[] 
            { 
                null, 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "anatoliy"),
                    new RelationalDatabaseValue("last_name", "oleynikov")
                },
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "UPDATE  SET first_name = 'anatoliy' AND last_name = 'oleynikov' WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
        [Test]
        public void IsBuildRequestCorrect3()
        {
            Assert.Throws<TargetInvocationException>(() =>
            {
                _buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[]
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
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[] 
            { 
                "humans", 
                new IDatabaseValue[] { },
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "grigoriy"),
                    new RelationalDatabaseValue("last_name", "fedotkin")
                } 
            })!;
            
            Assert.That(result == "UPDATE humans SET  WHERE first_name = 'grigoriy' AND last_name = 'fedotkin'");
        }
        
                
        [Test]
        public void IsBuildRequestCorrect5()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[] 
            { 
                "humans", 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "anatoliy"),
                    new RelationalDatabaseValue("last_name", "oleynikov")
                },
                null 
            })!;
            
            Assert.That(result == "UPDATE humans SET first_name = 'anatoliy' AND last_name = 'oleynikov'");
        }
        
        [Test]
        public void IsBuildRequestCorrect6()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[] 
            { 
                "humans", 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "anatoliy"),
                    new RelationalDatabaseValue("last_name", "oleynikov")
                },
                new RelationalDatabaseValue[] { } 
            })!;
            
            Assert.That(result == "UPDATE humans SET first_name = 'anatoliy' AND last_name = 'oleynikov'");
        }
        
        [Test]
        public void IsBuildRequestCorrect7()
        {
            var result = (string)_buildRequestMethodInfo.Invoke(_databaseDataUpdater, new object?[] 
            { 
                "huma''ns", 
                new IDatabaseValue[]
                {
                    new RelationalDatabaseValue("first_name", "anatoliy"),
                    new RelationalDatabaseValue("last_name", "oleynikov")
                },
                new RelationalDatabaseValue[] { } 
            })!;
            
            Assert.That(result == "UPDATE huma''''ns SET first_name = 'anatoliy' AND last_name = 'oleynikov'");
        }
    }
}