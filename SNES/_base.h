#pragma once

#include <string>

#define EXPORT extern "C" __declspec(dllexport)

typedef char sbyte;
typedef unsigned char byte;
typedef unsigned short ushort;
typedef unsigned int uint;

inline int mallocresize(int val)
{
	int i;
	for (i = 0; i < 31; ++i)
		if (1 << i >= val)
			return 1 << i;
	return 1 << 31;
}