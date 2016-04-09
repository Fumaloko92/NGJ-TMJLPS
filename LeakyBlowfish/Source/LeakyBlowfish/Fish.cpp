// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.

#include "LeakyBlowfish.h"
#include "Fish.h"
#include <Runtime/Engine/Classes/Components/ArrowComponent.h>

DEFINE_LOG_CATEGORY_STATIC(LogFish, Display, All);

AFish::AFish()
{
	static ConstructorHelpers::FObjectFinder<UStaticMesh> BallMesh(TEXT("/Game/Rolling/Meshes/BallMesh.BallMesh"));

	Acceleration = 5000;
	Friction = 5;

	RotationAcceleration = 250;
	RotationFriction = 5;

	InflationAcceleration = 100;
	InflationFriction = 5;
	MinInflation = 0.1f;
	MaxInflation = 1;

	// Create mesh component for the ball
	Ball = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Ball0"));
	Ball->SetStaticMesh(BallMesh.Object);
	Ball->BodyInstance.SetCollisionProfileName(UCollisionProfile::PhysicsActor_ProfileName);
	Ball->SetSimulatePhysics(true);
	Ball->BodyInstance.MassScale = 1.0f;
	Ball->BodyInstance.MaxAngularVelocity = 0.0f;
	Ball->SetNotifyRigidBodyCollision(true);
	Ball->SetEnableGravity(false);
	RootComponent = Ball;

	// Create a camera boom attached to the root (ball)
	SpringArm = CreateDefaultSubobject<USpringArmComponent>(TEXT("SpringArm0"));
	SpringArm->AttachTo(RootComponent);
	SpringArm->bDoCollisionTest = false;
	SpringArm->bAbsoluteRotation = true; // Rotation of the ball should not affect rotation of boom
	SpringArm->RelativeRotation = FRotator(-80.f, 0.f, 0.f);
	SpringArm->TargetArmLength = 750.f;
	SpringArm->bEnableCameraLag = false;
	SpringArm->CameraLagSpeed = 3.f;

	// Create a camera and attach to boom
	Camera = CreateDefaultSubobject<UCameraComponent>(TEXT("Camera0"));
	Camera->AttachTo(SpringArm, USpringArmComponent::SocketName);
	Camera->bUsePawnControlRotation = false; // We don't want the controller rotating the camera
}

void AFish::PostInitializeComponents()
{
	Super::PostInitializeComponents();

	ParticleSystem = FindComponentByClass<UParticleSystemComponent>();
	ParticleSystem->SetEmitterEnable(TEXT("Main"), false);

	Ball->SetAngularDamping(RotationFriction);
	Ball->SetLinearDamping(Friction);
}

void AFish::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	// set up gameplay key bindings
	InputComponent->BindAxis("MoveRight", this, &AFish::MoveRight);
	InputComponent->BindAxis("MoveForward", this, &AFish::MoveForward);
	InputComponent->BindAxis("Blow", this, &AFish::Blow);

	//InputComponent->BindAction("Jump", IE_Pressed, this, &AFish::Jump);
}

void AFish::MoveRight(float Val)
{
	Ball->AddTorque(FVector(0.0f, 0.0f, Val * RotationAcceleration * GetWorld()->DeltaTimeSeconds), NAME_None, true);
	//Ball->AddForce(FVector(0.0f, Val * Acceleration, 0.0f), NAME_None, true);
}

void AFish::MoveForward(float Val)
{
	//Ball->AddForce(FVector(Val * Acceleration, 0.0f, 0.0f), NAME_None, true);
}

void AFish::Jump()
{
	if(bCanJump)
	{
		const FVector Impulse = FVector(0.f, 0.f, JumpImpulse);
		Ball->AddImpulse(Impulse);
		bCanJump = false;
	}
}

void AFish::Blow(float Val)
{
	//float Inflate = FMath::Clamp(Val, 0.0f, 1.0f - Val);

	float Inflate = -1.0f;

	if (Val >= 0.9f)
	{
		Inflate = 1.0f;
	}
	else
	{
		Inflate = -(1.0f - (Val / 0.9f));
	}

	Inflation += InflationVelocity * GetWorld()->DeltaTimeSeconds;
	InflationVelocity += Inflate * InflationAcceleration * GetWorld()->DeltaTimeSeconds;
	InflationVelocity -= InflationVelocity * InflationFriction * GetWorld()->DeltaTimeSeconds;

	//UE_LOG( LogFish, Display, TEXT("Val: %f - Inflate: %f - Inflation: %f - InflationVelocity: %f"), Val, Inflate, Inflation, InflationVelocity )

	if (Inflation > MaxInflation)
	{
		Inflation = MaxInflation;
		InflationVelocity = 0;
	}
	else if (Inflation < 0)
	{
		Inflation = 0;
		InflationVelocity = 0;
	}

	if (Inflate < 0 && Inflation > 0)
	{
		float Propulsion = -Inflate;

		ParticleSystem->SetEmitterEnable(TEXT("Main"), true);
		
		Ball->AddForce(this->GetRootComponent()->GetForwardVector() * Propulsion * Acceleration * GetWorld()->DeltaTimeSeconds, NAME_None, true);
	}
	else 
	{
		ParticleSystem->SetEmitterEnable(TEXT("Main"), false);
	}

	if (MaxInflation > 0)
	{
		float Scale = MinInflation + FMath::Clamp(1.0f - MinInflation, 0.0f, 1.0f) * (Inflation / MaxInflation);
		Ball->SetRelativeScale3D(FVector(Scale));
		ParticleSystem->SetWorldScale3D(FVector(1.0f));
	}
}


void AFish::NotifyHit(class UPrimitiveComponent* MyComp, class AActor* Other, class UPrimitiveComponent* OtherComp, bool bSelfMoved, FVector HitLocation, FVector HitNormal, FVector NormalImpulse, const FHitResult& Hit)
{
	Super::NotifyHit(MyComp, Other, OtherComp, bSelfMoved, HitLocation, HitNormal, NormalImpulse, Hit);

	bCanJump = true;
}
