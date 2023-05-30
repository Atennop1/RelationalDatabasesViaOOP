using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests
{
    public sealed class RelationalDatabaseValueTests
    {
        [Test]
        public void IsCreationCorrect1() 
            => Assert.Throws<ArgumentNullException>(() => { var data = new RelationalDatabaseValue("first_name", null); });

        [Test]
        public void IsCreationCorrect2()
            => Assert.Throws<ArgumentNullException>(() => { var data = new RelationalDatabaseValue(null, 1); });

        [Test]
        public void IsCreationCorrect3()
        {
            var data = new RelationalDatabaseValue("first_name", 1);
            Assert.That(() => data.Get().ToString() == "1");
        }
        
        [Test]
        public void IsCreationCorrect4()
        {
            var data = new RelationalDatabaseValue("first_name", "james");
            Assert.That(() => data.Get().ToString() == "'james'");
        }

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