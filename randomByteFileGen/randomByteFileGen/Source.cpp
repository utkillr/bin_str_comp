#include <iostream>
#include <random>
#include <time.h>
#include <fstream>

using namespace std;

int main() {
	ofstream file;
	file.open("generated.txt");
	srand(time(NULL));

	cout << "How many strings? ";
	int amount;
	cin >> amount;

	cout << "How many symbols in string? ";
	int strlen;
	cin >> strlen;

	if (strlen < 5) strlen = 5;

	for (int i = 0; i < amount; i++) {
		int length = rand() % (strlen - 2) + 3;
		for (int j = 0; j < length; j++) {
			file << (rand() % 2 == 0 ? "0" : "1");
		}
		file << endl;
		if ((i+1) % 10000 == 0) cout << i+1 << " string generated" << endl;
	}
}