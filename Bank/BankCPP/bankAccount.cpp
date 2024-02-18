#include "bankAccount.h"

#pragma region BankAccount
	std::int32_t BankAccount::getBalanceInternal() const { return balanceInternal; }
	void BankAccount::setBalanceInternal(std::int32_t balance) { balanceInternal = balance;  }

	std::int32_t BankAccount::getBalance() const { return state->getBalance(); }

	BankAccount::BankAccount()
	{
		balanceInternal = 0;
		state = new UnauthorizedState{ this };
	}

	BankAccount::~BankAccount() { delete state; }

	void BankAccount::authorize() { changeStateTo(state->authorize()); }
	void BankAccount::deposit(std::uint16_t amount) { changeStateTo(state->deposit(amount)); }
	void BankAccount::withdraw(std::uint16_t amount) { changeStateTo(state->withdraw(amount)); }
	void BankAccount::changeStateTo(State* state)
	{
		if (this->state == state) return;

		delete this->state;
		this->state = state;
	}
#pragma endregion BankAccount


#pragma region State
	BankAccount::State::State(BankAccount* owner) : owner{ owner } { }
#pragma endregion State


#pragma region UnauthorizedState
	BankAccount::UnauthorizedState::UnauthorizedState(BankAccount* owner) : State{ owner } { }

	int BankAccount::UnauthorizedState::getBalance() const { throw std::runtime_error{ "Unauthorized access of account!" }; }
	BankAccount::State* BankAccount::UnauthorizedState::authorize() { return new NormalState(owner); }
	BankAccount::State* BankAccount::UnauthorizedState::deposit(std::uint16_t amount) { throw std::runtime_error{ "Unauthorized access of account!" }; }
	BankAccount::State* BankAccount::UnauthorizedState::withdraw(std::uint16_t amount) { throw std::runtime_error{ "Unauthorized access of account!" }; }
#pragma endregion UnauthorizedState


#pragma region NormalState
	BankAccount::NormalState::NormalState(BankAccount* owner) : State{ owner } { }

	int BankAccount::NormalState::getBalance() const { return owner->getBalanceInternal(); }
	BankAccount::State* BankAccount::NormalState::authorize() { return this; }
	BankAccount::State* BankAccount::NormalState::deposit(std::uint16_t amount)
	{
		owner->setBalanceInternal(owner->getBalanceInternal() + amount);
		return this;
	}
	BankAccount::State* BankAccount::NormalState::withdraw(std::uint16_t amount)
	{
		owner->setBalanceInternal(owner->getBalanceInternal() - amount);
			
		if (owner->getBalanceInternal() < 0) return new OverdraftedState{ owner };

		return this;
	}
#pragma endregion NormalState


#pragma region OverdraftedState
	BankAccount::OverdraftedState::OverdraftedState(BankAccount* owner) : State{ owner } { }

	int BankAccount::OverdraftedState::getBalance() const { return owner->getBalanceInternal(); }
	BankAccount::State* BankAccount::OverdraftedState::authorize() { return this; }
	BankAccount::State* BankAccount::OverdraftedState::deposit(std::uint16_t amount)
	{
		owner->setBalanceInternal(owner->getBalanceInternal() + amount);

		if (owner->getBalanceInternal() >= 0) return new NormalState{ owner };

		return this;
	}
	BankAccount::State* BankAccount::OverdraftedState::withdraw(std::uint16_t amount) { return this; }
#pragma region OverdraftedState