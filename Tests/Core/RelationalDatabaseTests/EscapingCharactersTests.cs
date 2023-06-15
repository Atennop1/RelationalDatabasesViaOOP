using System.Reflection;
using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests
{
    public sealed class EscapingCharactersTests
    {
        private RelationalDatabase _database;

        [OneTimeSetUp]
        public void Setup() 
            => _database = new RelationalDatabasesFactory().Create();

        [Test]
        public void IsEscapingCharactersCorrect1()
        {
            Assert.That(() =>
            {
                var method = _database.GetType().GetMethod("AddEscapingCharactersToString", BindingFlags.NonPublic | BindingFlags.Instance);
                var resultRequest = (string)method?.Invoke(_database, new object[] { "select * from !@#$%^&*()_+\\/,\"'" });
                return resultRequest == "select * from !@#$%^&*()_+\\/,\"''";
            });
        }

        [Test]
        public void IsEscapingCharactersCorrect2()
        {
            Assert.Throws<TargetInvocationException>(() =>
            {
                var method = _database.GetType().GetMethod("AddEscapingCharactersToString", BindingFlags.NonPublic | BindingFlags.Instance);
                method?.Invoke(_database, new object[] { null });
            });
        }
    }
}