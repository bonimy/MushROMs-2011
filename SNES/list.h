#pragma once

#include "_base.h"

#define LISTSTARTLEN 0x20

template<class T>
class list
{
public:
	T **data;
	int count, capacity;

	list();
	~list();

	T *get(int index);
	void listalloc(int size);
};

template<class T>
list<T>::list()
{
	this->data = (T**)malloc(LISTSTARTLEN << 2);
	do
	{
		this->data[this->capacity] = new T();
		++this->capacity;
	}while (this->capacity < LISTSTARTLEN);
	this->count = 0;
}

template<class T>
list<T>::~list()
{
	do
	{
		delete this->data[this->capacity];
		--this->capacity;
	}while (this->capacity >= 0);
	free(this->data);
}
	
template<class T>
T *list<T>::get(int index)
{
	if (index >= this->capacity)
		listalloc(index + 1);
	if (index >= this->count)
		this->count = index + 1;
	return this->data[index];
}

template<class T>
void list<T>::listalloc(int size)
{
	size = mallocresize(size);
	this->data = (T**)realloc(T, size << 2);
	do
	{
		this->data[this->capacity] = new T();
		++this->capacity;
	}while (this->capacity < size);
}