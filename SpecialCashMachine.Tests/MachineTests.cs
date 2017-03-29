using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace SpecialCashMachine.Tests
{
    [TestFixture]
    public class MachineTests
    {
        private CashMachine _sut;
        private CassetteBuilder _cassetteBuilder = new CassetteBuilder();
        private readonly decimal _startingBalance = 4638m;

        [SetUp]
        public void Init()
        {
            _cassetteBuilder = new CassetteBuilder();

            _sut = new CashMachine(_cassetteBuilder.WithDefaultCurrency().Build());
        }

        [Test]
        public void ShouldHaveBalanceProperty()
        {
            Assert.That(_sut, Has.Property("Balance"));
        }

        [Test]
        public void ShouldHaveCassetteProperty()
        {
            Assert.That(_sut, Has.Property("Cassettes"));
        }

        [Test]
        public void ShouldHaveWithdrawalProperty()
        {
            Assert.That(_sut, Has.Property("WithdrawalAmounts"));
        }

        [Test]
        public void ShouldHaveCassetteObjectPassedToConstructor()
        {
            Assert.Throws<System.ArgumentException>(()=> new CashMachine(null));
        }

        [Test]
        public void ShouldCalculateTotalBalance()
        {
           Assert.IsTrue(_sut.Balance == _startingBalance);
        }
        
        [Test]
        public void ShouldDispenseLeastNumber()
        {
            var expectedBalance = (_startingBalance - 120.23m);
          _sut.Dispense(120.23m, Algorithm.LeastItems);
            Assert.IsTrue(expectedBalance == _sut.Balance);
        }

        [Test]
        public void ShouldProduceMessage()
        {
            _sut.Dispense(120.23m, Algorithm.LeastItems);

            Assert.IsTrue(_sut.WithdrawalAmounts[50.00m] == 2);
            Assert.IsTrue(_sut.WithdrawalAmounts[20.00m] == 1);
            Assert.IsTrue(_sut.WithdrawalAmounts[0.20m] == 1);
            Assert.IsTrue(_sut.WithdrawalAmounts[0.02m] == 1);
            Assert.IsTrue(_sut.WithdrawalAmounts[0.01m] == 1);
        }


        [Test]
        public void ShouldDispenseFavoured()
        {
            var expectedBalance = (_startingBalance - 86.99m);
            _sut.Dispense(86.99m, Algorithm.FavouredDenomination);
            Assert.IsTrue(expectedBalance == _sut.Balance);
        }

        [Test]
        public void ShouldFavour20Denominations()
        {
            _sut.Dispense(120.00m, Algorithm.FavouredDenomination);

            Assert.IsTrue(_sut.WithdrawalAmounts[20.00m] == 6);
        }

        [Test]
        public void ShouldStillSelectIfAmountLessThanRequestedFavouredDenomination()
        {
            _sut.Dispense(10.00m, Algorithm.FavouredDenomination);
            Assert.IsTrue(_sut.WithdrawalAmounts[10.00m] == 1);
        }

        [Test]
        public void ShouldCalculateTranscationsODifferentTypes()
        {
            _sut.Dispense(10.00m, Algorithm.FavouredDenomination);
            _sut.Dispense(28.00m, Algorithm.LeastItems);

            Assert.IsTrue(_sut.Balance == 4600.00m);
        }

    }
}
