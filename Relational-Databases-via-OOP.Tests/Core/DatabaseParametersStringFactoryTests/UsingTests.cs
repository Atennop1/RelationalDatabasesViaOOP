using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.DatabaseParametersStringFactoryTests
{
    public sealed class UsingTests
    {
        private IDatabaseParametersStringFactory _factory;

        [SetUp]
        public void Setup()
            => _factory = new DatabaseParametersStringFactory();

        [Test]
        public void IsCreatingCorrect1()
            => Assert.Throws<ArgumentNullException>(() => _factory.Create(null, " AND "));
        
        [Test]
        public void IsCreatingCorrect2()
            => Assert.Throws<ArgumentNullException>(() => _factory.Create(new string[] { }, null));
        
        [Test]
        public void IsCreatingCorrect3()
            => Assert.That(_factory.Create(new string[] { }, " AND "), Is.EqualTo(string.Empty));
        
        [Test]
        public void IsCreatingCorrect4()
            => Assert.That(_factory.Create(new[] { "hello", "goodbye" }, string.Empty), Is.EqualTo("hellogoodbye"));
        
        [Test]
        public void IsCreatingCorrect5()
            => Assert.That(_factory.Create(new[] { "hello", "goodbye" }, " AND "), Is.EqualTo("hello AND goodbye"));
    }
}