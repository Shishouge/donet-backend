#include "code.h"

BMCode_EXPORTS char* EnCode(char* str)
{
	int f = 1;
	for (int i = 0; i < strlen(str); i++)
	{
		str[i] = str[i] + f;
		f = -f;
	}
	return str;
}

BMCode_EXPORTS char* DeCode(char* str)
{
	int f = -1;
	for (int i = 0; i < strlen(str); i++)
	{
		str[i] = str[i] + f;
		f = -f;
	}
	return str;
}


code::code()
{
}

code::~code()
{
}

