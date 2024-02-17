#include <stdexcept>

class Bank
{
private:
	class State;
	struct UnauthorizedState;
	struct NormalState;
	struct OverdraftedState;

	State* state;

protected:
	std::int32_t balanceInternal;

public:
	std::int32_t getBalance() const { return state->getBalance(); }

	Bank()
	{
		balanceInternal = 0;
		state = new UnauthorizedState{ this };
	}

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
		Bank* owner;

		State(Bank* owner) : owner{ owner } { }

	public:
		virtual int getBalance() const = 0;
		virtual State* authorize() = 0;
		virtual State* deposit(std::uint16_t amount) = 0;
		virtual State* withdraw(std::uint16_t amount) = 0;
	};

	struct UnauthorizedState : public State
	{
		UnauthorizedState(Bank* owner) : State{ owner } { }

		int getBalance() const override { throw std::invalid_argument{ "Unauthorized access of account!" }; }
		State* authorize() { return new NormalState(owner); }
		State* deposit(std::uint16_t amount) override { throw std::invalid_argument{ "Unauthorized access of account!" }; }
		State* withdraw(std::uint16_t amount) override { throw std::invalid_argument{ "Unauthorized access of account!" }; }
	};

	struct NormalState : public State
	{
		NormalState(Bank* owner) : State{ owner } { }

		int getBalance() const override { return owner->balanceInternal; }
		State* authorize() override { return this; }
		State* deposit(std::uint16_t amount) override
		{
			owner->balanceInternal += amount;
			return this;
		}
		State* withdraw(std::uint16_t amount) override
		{
			owner->balanceInternal -= amount;
			
			if (owner->balanceInternal < 0)
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
		OverdraftedState(Bank* owner) : State{ owner } { }

		int getBalance() const override { return owner->balanceInternal; }
		State* authorize() { return this; }
		State* deposit(std::uint16_t amount) override
		{
			owner->balanceInternal += amount;

			if (owner->balanceInternal >= 0)
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