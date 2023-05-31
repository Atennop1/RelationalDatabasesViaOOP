using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.RelationalDatabaseValueTests
{
    public sealed class EscapingCharactersTests
    {
        [Test]
        public void IsEscapingCharactersWorksCorrect1()
        {
            var data = new RelationalDatabaseValue("'first'_name''", 1);
            Assert.That(() => data.Name == "''first''_name''''");
        }
        
        [Test]
        public void IsEscapingCharactersWorksCorrect2()
        {
            var data = new RelationalDatabaseValue("first_name", "'jam'es'");
            TestContext.Out.WriteLine(data.Get().ToString());
            Assert.That(() => data.Get().ToString() == "'''jam''es'''");
        }
        
        [Test]
        public void IsEscapingCharactersWorksCorrect3()
        {
            var data = new RelationalDatabaseValue("'first'_name''", "'jam'es'");
            Assert.That(() => data.Name == "''first''_name''''" && data.Get().ToString() == "'''jam''es'''");
        }
    }
}