using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseValueTests
{
    public sealed class CreationsTests
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
    }
}