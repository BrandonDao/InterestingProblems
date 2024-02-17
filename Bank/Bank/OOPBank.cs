namespace Bank
{
    public class OOPBank
    {
        private abstract class State(OOPBank owner)
        {
            protected OOPBank owner = owner;

            public abstract int Balance { get; }
            public abstract State Authenticate();
            public abstract State Deposit(ushort amount);
            public abstract State Withdraw(ushort amount);
        }
        private class AuthorizedState(OOPBank owner) : State(owner)
        {
            public override int Balance => owner.BalanceInternal;
            public override State Authenticate() => this;
            public override State Deposit(ushort amount)
            {
                owner.BalanceInternal += amount;
                return this;
            }
            public override State Withdraw(ushort amount)
            {
                owner.BalanceInternal -= amount;

                if (owner.Balance < 0) return new LockedState(owner);

                return this;
            }
        }
        private class UnauthenticatedState(OOPBank owner) : State(owner)
        {
            public override int Balance => throw new FunctionalBank.AuthenticationException();
            public override State Authenticate() => new AuthorizedState(owner);
            public override State Deposit(ushort _) => throw new FunctionalBank.AuthenticationException();
            public override State Withdraw(ushort _) => throw new FunctionalBank.AuthenticationException();
        }
        private class LockedState(OOPBank owner) : State(owner)
        {
            public override int Balance => owner.BalanceInternal;
            public override State Authenticate() => owner.state = new AuthorizedState(owner);
            public override State Deposit(ushort amount)
            {
                owner.BalanceInternal += amount;

                if(owner.Balance >= 0) return new AuthorizedState(owner);

                return this;
            }
            public override State Withdraw(ushort amount) => this;
        }


        private State state;
        protected int BalanceInternal { get; set; }
        public int Balance { get => state.Balance; }

        public OOPBank()
        {
            state = new UnauthenticatedState(owner: this);
        }

        public void Authenticate() => state = state.Authenticate();
        public void Deposit(ushort amount) => state = state.Deposit(amount);
        public void Withdraw(ushort amount) => state = state.Withdraw(amount);

    }
}