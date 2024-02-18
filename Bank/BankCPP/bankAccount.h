#include <cstdint>
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
		std::int32_t getBalanceInternal() const;
		void setBalanceInternal(std::int32_t balance);

	public:
		std::int32_t getBalance() const;

		BankAccount();
		~BankAccount();

		void authorize();
		void deposit(std::uint16_t amount);
		void withdraw(std::uint16_t amount);
	private:
		void changeStateTo(State* state);

	private:
		class State
		{
			protected:
				BankAccount* owner;

				State(BankAccount* owner);

			public:
				virtual int getBalance() const = 0;
				virtual State* authorize() = 0;
				virtual State* deposit(std::uint16_t amount) = 0;
			virtual State* withdraw(std::uint16_t amount) = 0;
		};

		struct UnauthorizedState : public State
		{
			UnauthorizedState(BankAccount* owner);

			int getBalance() const override;
			State* authorize() override;
			State* deposit(std::uint16_t amount) override;
			State* withdraw(std::uint16_t amount) override;
		};

		struct NormalState : public State
		{
			NormalState(BankAccount* owner);

			int getBalance() const override;
			State* authorize() override;
			State* deposit(std::uint16_t amount) override;
			State* withdraw(std::uint16_t amount) override;
		};

		struct OverdraftedState : public State
		{
			OverdraftedState(BankAccount* owner);

			int getBalance() const override;
			State* authorize() override;
			State* deposit(std::uint16_t amount) override;
			State* withdraw(std::uint16_t amount) override;
		};
};