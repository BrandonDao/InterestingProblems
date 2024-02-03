#include <iostream>
#include <string>

int main()
{
	const int n = 30;

	std::string outputLookup[]{ "", "", "fizz", "buzz" };
	void* jumpTable[2];

	void* loopPtr = nullptr, *endPtr = nullptr;

	__asm
	{
		mov [loopPtr], offset loopLabel
		mov [endPtr], offset endLabel
	}

	jumpTable[0] = loopPtr;
	jumpTable[1] = endPtr;

	int i = 1;

loopLabel:
	outputLookup[1] = std::to_string(i);

	std::uint8_t fizzIdx = (i % 3 + 1) >> 1;
	std::uint8_t buzzIdx = ((((i % 5) + 1) >> 1) + 1) >> 1;

	std::cout
		<< outputLookup[(1 - fizzIdx) * 2]
		<< outputLookup[(1- buzzIdx) * 3]
		<< outputLookup[buzzIdx & fizzIdx]
		<< std::endl;

	void* gotoMe = jumpTable[i / n];
	i++;
	
	__asm jmp [gotoMe];

endLabel:
	;
}