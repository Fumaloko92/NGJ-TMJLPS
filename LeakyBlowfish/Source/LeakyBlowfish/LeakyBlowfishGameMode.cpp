// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.

#include "LeakyBlowfish.h"
#include "LeakyBlowfishGameMode.h"
#include "Fish.h"

ALeakyBlowfishGameMode::ALeakyBlowfishGameMode()
{
	// set default pawn class to our ball
	DefaultPawnClass = AFish::StaticClass();
}
