#include <string>
#include <array>
#include <list>

static std::string find_unique(std::list<std::string> input)
{
	std::list<std::array<int, 26>> charCountsLookup;
	auto itr = input.cbegin();

	while (itr != input.cend())
	{
		auto currCharCounter = charCountsLookup.begin();

		for (int i = 0; i < itr->length(); i++)
		{
			if (charCountsLookup.size() <= i)
			{
				charCountsLookup.emplace_back(std::array<int, 26>{});
				currCharCounter = --charCountsLookup.end();
			}
			(*currCharCounter)[itr->at(i) - 'a']++;
			currCharCounter++;
		}
		itr++;
	}
	
	std::string output{};

	for (const std::array<int, 26>& charCounts : charCountsLookup)
	{
		int minIdx = -1;
		for (int i = 0; i < 26; i++)
		{
			if ((charCounts[i] & 1) == 0) continue;
			
			minIdx = i;
			break;
		}

		for (int i = minIdx + 1; i < 26; i++)
		{
			if ((charCounts[i] & 1) == 0 || charCounts[i] >= charCounts[minIdx]) continue;

			minIdx = i;
		}

 		output += minIdx + 'a';
	}

	return output;
}

int main()
{
	/*
	* Duplicates come in pairs
	* All words are lowercase (a-z)
	*/
	std::list<std::string> input{ "abc", "abc", "cba", "eab", "cab", "eab", "cab", "z", "z" };
	
	auto result = find_unique(input);

	;
}