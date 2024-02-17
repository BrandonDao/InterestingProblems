namespace Bank
{
    public class FunctionalBank
    {
        internal class AuthenticationException : Exception
        {
            public AuthenticationException() { }
            public AuthenticationException(string? message) : base(message) { }
        }

        private static Action ThrowsError => () => throw new AuthenticationException();
        private static Action DoesNotThrow => () => { };

        private Action checkAuthentication;
        private Action<int> depositAction;
        private Action<int> withdrawAction;

        private int balance;
        public int Balance
        {
            get { checkAuthentication.Invoke(); return balance; }
        }

        public FunctionalBank()
        {
            checkAuthentication = ThrowsError;
            depositAction = BaseDeposit;
            withdrawAction = BaseWithdraw;
        }

        public void Authenticate() => checkAuthentication = DoesNotThrow;

        public void Deposit(int amount)
        {
            checkAuthentication.Invoke();
            depositAction.Invoke(amount);
        }

        public void Withdraw(int amount)
        {
            checkAuthentication.Invoke();
            withdrawAction.Invoke(amount);
        }

        private void BaseDeposit(int amount) => balance += amount;
        private void LockedDeposit(int amount)
        {
            BaseDeposit(amount);

            if (balance >= 0)
            {
                depositAction = BaseDeposit;
                withdrawAction = BaseWithdraw;
            }
        }

        private void BaseWithdraw(int amount)
        {
            balance -= amount;

            if (balance < 0)
            {
                depositAction = LockedDeposit;
                withdrawAction = LockedWithdraw;
            }
        }

        private void LockedWithdraw(int amount) { }
    }
}