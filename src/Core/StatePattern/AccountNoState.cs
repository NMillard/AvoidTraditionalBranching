using System;

namespace DemoTests.StatePattern.NoState {
    
    /*
     * Simple account class
     */
    public class Account {
        public Account(string iban) {
            Iban = iban;
            State = AccountState.Open;
        }
        
        public string Iban { get; }
        
        // Three possible states: Open, Closed, or Frozen
        public AccountState State { get; internal set; }
        public decimal CurrentBalance { get; internal set; }
        public bool IsOverdraftAllowed { get; internal set; }
        
        public void WithdrawAmount(decimal amount) {
            if (State == AccountState.Open) {
                if (amount > CurrentBalance) {
                    if (IsOverdraftAllowed) {
                        CurrentBalance -= amount;
                    } else {
                        throw new InvalidOperationException($"Account {Iban} does not allow overdraft.");
                    }
                } else {
                    CurrentBalance -= amount;
                }
            } else if (State == AccountState.Closed) {
                throw new InvalidOperationException($"Cannot withdraw money because account {Iban} is closed.");
            } else if (State == AccountState.Frozen) {
                throw new InvalidOperationException($"Cannot withdraw money because account {Iban} is frozen.");
            }
        }
    }

    public enum AccountState {
        Open,
        Closed,
        Frozen,
    }
}