// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Kismet/BlueprintFunctionLibrary.h"
#include "EventDistAlgo.generated.h"

/**
 * param numEvents = ()
 * Shuffles inputs, representing events, as an output queue (uses array for simplicity of management in UE - caller will enforce)
 * return = array populated with events in random order
 */
UCLASS()
class LATENITEUNREAL_API UEventDistAlgo : public UBlueprintFunctionLibrary
{
	GENERATED_BODY()
public:
		UFUNCTION(BlueprintCallable, Category = "Algorithms")
		static int* EventDistAlgo(int numEvents);
};
