#pragma once
#include <iostream>
#include<string>
#include "E:\大三下\.NET\Project\Backend\BloodManagement\BMCode\code.h"//引用库声明对应文件路径
public ref class InvokeCon
{
public:
	InvokeCon();

	char* EnCodeCli(char* str);
	char* DeCodeCli(char* str);
};

