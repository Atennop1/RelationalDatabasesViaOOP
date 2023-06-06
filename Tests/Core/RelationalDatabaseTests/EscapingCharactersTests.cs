using System.Reflection;
using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.Tests.Core.RelationalDatabaseTests
{
    public sealed class EscapingCharactersTests
    {
        [Test]
        public void IsEscapingCharactersCorrect()
        {
            var relationalDatabase = new RelationalDatabasesFactory().Create();
            
            Assert.That(() =>
            {
                var method = relationalDatabase.GetType().GetMethod("AddEscapingCharactersToString", BindingFlags.NonPublic | BindingFlags.Instance);
                var resultRequest = (string)method?.Invoke(relationalDatabase, new object[] { "select * from !@#$%^&*()_+\\/,\"'" })!;
                return resultRequest == "select * from !@#$%^&*()_+\\/,\"''";
            });
        }
    }
}