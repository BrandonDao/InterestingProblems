#include <stdexcept>

class BankAccount
{
private:
	class State;
	struct UnauthorizedState;
	struct NormalState;
	struct OverdraftedState;

	State* state;

protected:
	std::int32_t balanceInternal;
	std::int32_t getBalanceInternal() inline const { return balanceInternal; }
	void setBalanceInternal(std::int32_t balance) inline { balanceInternal = balance;  }

public:
	std::int32_t getBalance() const { return state->getBalance(); }

	BankAccount()
	{
		balanceInternal = 0;
		state = new UnauthorizedState{ this };
	}

	~BankAccount() { delete state; }

	void authorize()
	{
		state = state->authorize();
	}
	void deposit(std::uint16_t amount)
	{
		state = state->deposit(amount);
	}
	void withdraw(std::uint16_t amount)
	{
		state = state->withdraw(amount);
	}

private:
	class State
	{
	protected:
		BankAccount* owner;

		State(BankAccount* owner) : owner{ owner } { }

	public:
		virtual int getBalance() const = 0;
		virtual State* authorize() = 0;
		virtual State* deposit(std::uint16_t amount) = 0;
		virtual State* withdraw(std::uint16_t amount) = 0;
	};

	struct UnauthorizedState : public State
	{
		UnauthorizedState(BankAccount* owner) : State{ owner } { }

		int getBalance() const override { throw std::runtime_error{ "Unauthorized access of account!" }; }
		State* authorize()
		{
			auto temp = owner;
			delete this;
			return new NormalState(temp);
		}
		State* deposit(std::uint16_t amount) override { throw std::runtime_error{ "Unauthorized access of account!" }; }
		State* withdraw(std::uint16_t amount) override { throw std::runtime_error{ "Unauthorized access of account!" }; }
	};

	struct NormalState : public State
	{
		NormalState(BankAccount* owner) : State{ owner } { }

		int getBalance() const override { return owner->getBalanceInternal(); }
		State* authorize() override { return this; }
		State* deposit(std::uint16_t amount) override
		{
			owner->setBalanceInternal(owner->getBalanceInternal() + amount);
			return this;
		}
		State* withdraw(std::uint16_t amount) override
		{
			owner->setBalanceInternal(owner->getBalanceInternal() - amount);
			
			if (owner->getBalanceInternal() < 0)
			{
				auto temp = new OverdraftedState{owner};
				delete this;
				return temp;
			}
			return this;
		}
	};

	struct OverdraftedState : public State
	{
		OverdraftedState(BankAccount* owner) : State{ owner } { }

		int getBalance() const override { return owner->getBalanceInternal(); }
		State* authorize() { return this; }
		State* deposit(std::uint16_t amount) override
		{
			owner->setBalanceInternal(owner->getBalanceInternal() + amount);

			if (owner->getBalanceInternal() >= 0)
			{
				auto temp = new NormalState{ owner };
				delete this;
				return temp;
			}
			return this;
		}
		State* withdraw(std::uint16_t amount) override { return this; }
	};
};