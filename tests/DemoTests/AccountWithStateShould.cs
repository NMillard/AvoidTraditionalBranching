using System;
using DemoTests.StatePattern.WithState;
using Xunit;

namespace DemoTests {
    public class AccountWithStateShould {
        [Fact]
        public void WithdrawMoney() {
            var account = new Account("DK001", false) { CurrentBalance = 10 };

            // Act
            account.WithdrawAmount(10);
            
            Assert.Equal(0, account.CurrentBalance);
        }

        [Fact]
        public void WithdrawMoneyFromAccountWhenOverdraftIsAllowed() {
            var account = new Account("DK001", true) { CurrentBalance = 10 };

            // Act
            account.WithdrawAmount(11);
            
            Assert.Equal(-1, account.CurrentBalance);
        }

        [Fact]
        public void FailWhenOverdraftIsNotAllowed() {
            var account = new Account("DK001", false) { CurrentBalance = 10 };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(11));
        }

        [Fact]
        public void FailWhenAccountIsClosed() {
            var account = new Account("DK001", false) { AccountState = new Closed() };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(11));
        }

        [Fact]
        public void FailWhenAccountIsFrozen() {
            var account = new Account("DK001", false) { AccountState = new Frozen() };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(11));
        }
    }
}