using System;

namespace DemoTests.StatePattern.WithState {
    public class Account {
        public Account(string iban, bool isOverdraftAllowed) {
            Iban = iban;
            AccountState = new Open(isOverdraftAllowed);
        }
        
        public string Iban { get; }
        public decimal CurrentBalance { get; internal set; }
        
        // Missing state
        public AccountState AccountState { get; internal set; }

        public void WithdrawAmount(decimal amount) => AccountState.WithdrawAmount(this, amount);
    }

    public abstract class AccountState {
        public abstract void WithdrawAmount(Account account, decimal amount);
    }

    public class Open : AccountState {
        private readonly bool isOverdraftAllowed;

        public Open(bool isOverdraftAllowed) {
            this.isOverdraftAllowed = isOverdraftAllowed;
        }
        
        public override void WithdrawAmount(Account account, decimal amount) {
            if (amount > account.CurrentBalance && !isOverdraftAllowed)
                throw new InvalidOperationException($"Account {account.Iban} does not allow overdraft.");
            
            account.CurrentBalance -= amount;
        }
    }

    public class Closed : AccountState {
        public override void WithdrawAmount(Account account, decimal amount) {
            throw new InvalidOperationException($"Cannot withdraw money because account {account.Iban} is closed.");
        }
    }
    
    public class Frozen : AccountState {
        public override void WithdrawAmount(Account account, decimal amount) {
            throw new InvalidOperationException($"Cannot withdraw money because account {account.Iban} is frozen.");
        }
    }
}