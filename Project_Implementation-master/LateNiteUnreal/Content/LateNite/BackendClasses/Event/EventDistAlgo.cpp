// Fill out your copyright notice in the Description page of Project Settings.


#include "EventDistAlgo.h"

int* UEventDistAlgo::EventDistAlgo(int numEvents)
{
	int* ordering;
	ordering = new int[numEvents];
	for (int i = 0; i < numEvents; i++) {
		ordering[i] = i;
	}
	for (int i = numEvents - 1; i > 0; i--) {
		int j = 1 + (rand() % (i - 1));
		int swap = ordering[i];
		ordering[i] = ordering[j];
		ordering[j] = swap;
	}
	return ordering;
}