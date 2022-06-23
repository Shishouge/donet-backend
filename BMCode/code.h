#pragma once

#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include<string>

using namespace std;

#ifdef BMCodeDLL_EXPORTS
#define BMCode_EXPORTS __declspec(dllexport)
#else
#define BMCode_EXPORTS __declspec(dllimport)
#endif

extern "C" BMCode_EXPORTS char* EnCode(char* str);

extern "C" BMCode_EXPORTS char* DeCode(char* str);



class code
{

public:
	code();
	~code();

};


