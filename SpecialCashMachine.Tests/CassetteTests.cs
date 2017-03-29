using System.Data.Odbc;
using Moq.Language;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SpecialCashMachine.Tests
{
    [TestFixture]
    public class CassetteTests
    {
        private ICassette _sut ;

        [SetUp]
        public void Init()
        {
            _sut = new Cassette();
        }

        [Test]
        public void ShouldHaveDenominationProperty()
        {
            Assert.That(_sut, Has.Property("Denomination"));
        }

        [Test]
        public void ShouldHaveQuantityProperty()
        {
            Assert.That(_sut, Has.Property("Quantity"));
        }


        [Test]
        public void ShouldHaveCurrencyProperty()
        {
            Assert.That(_sut, Has.Property("Currency"));
        }

        [Test]
        public void ShouldHaveTotalProperty()
        {
            Assert.That(_sut, Has.Property("Total"));
        }

        [Test]
        public void ShouldDefaultToGBP()
        {
            Assert.IsTrue(_sut.Currency == "GBP");
        }


    }
}
