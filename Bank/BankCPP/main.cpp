#include "bank.cpp"
#include <iostream>

int main()
{
	Bank bank{};

	bank.authorize();
	bank.deposit(100);
	bank.withdraw(50);
	bank.withdraw(100);
	bank.withdraw(100);
	bank.deposit(50);

	for(int i = 0; i < 217; i++)
	{
		bank.deposit(100);
		std::cout << bank.getBalance() << std::endl;
		bank.withdraw(1000);
		std::cout << bank.getBalance() << std::endl;
		bank.deposit(900);
		std::cout << bank.getBalance() << std::endl;
	}
}