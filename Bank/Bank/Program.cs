namespace Bank
{
    internal class Program
    {
        static void Main()
        {
            var bank = new OOPBank();

            try
            {
                var a = bank.Balance;
            }
            catch { }

            try
            {
                bank.Deposit(10000);
            }
            catch { }

            bank.Authenticate();
            bank.Deposit(5);           //  5
            bank.Withdraw(100);         // -95
            bank.Deposit(94);           // -1
            bank.Withdraw(100);         // -1
            bank.Deposit(1);            //  0
            bank.Withdraw(100);         // -100

            ;
        }
    }
}