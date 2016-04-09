// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.
#pragma once
#include "GameFramework/Pawn.h"
#include "Fish.generated.h"

UCLASS(config=Game)
class AFish : public APawn
{
	GENERATED_BODY()

	/** StaticMesh used for the ball */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Ball, meta = (AllowPrivateAccess = "true"))
	class UStaticMeshComponent* Ball;

	/** Spring arm for positioning the camera above the ball */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Ball, meta = (AllowPrivateAccess = "true"))
	class USpringArmComponent* SpringArm;

	/** Camera to view the ball */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Ball, meta = (AllowPrivateAccess = "true"))
	class UCameraComponent* Camera;

	UPROPERTY()
	class UParticleSystemComponent* ParticleSystem;

public:
	AFish();

	/** Vertical impulse to apply when pressing jump */
	UPROPERTY(EditAnywhere, Category=Ball)
	float JumpImpulse;

	/** Torque to apply when trying to roll ball */
	UPROPERTY(EditAnywhere, Category=Ball)
	float RollTorque;

	/** Indicates whether we can currently jump, use to prevent double jumping */
	bool bCanJump;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float Acceleration;

	UPROPERTY(EditAnywhere, Category = Ball)
	float Friction;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float RotationAcceleration;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float RotationFriction;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float MinInflation;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float MaxInflation;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float InflationAcceleration;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Ball)
	float InflationFriction;

	UPROPERTY()
	float Inflation;

protected:

	UPROPERTY()
	float InflationVelocity;

	/** Called for side to side input */
	void MoveRight(float Val);

	/** Called to move ball forwards and backwards */
	void MoveForward(float Val);

	/** Handle jump action. */
	void Jump();

	void Blow(float Val);

	// AActor interface
	virtual void NotifyHit(class UPrimitiveComponent* MyComp, class AActor* Other, class UPrimitiveComponent* OtherComp, bool bSelfMoved, FVector HitLocation, FVector HitNormal, FVector NormalImpulse, const FHitResult& Hit) override;
	// End of AActor interface

	// APawn interface
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;
	// End of APawn interface

	virtual void PostInitializeComponents() override;

public:
	/** Returns Ball subobject **/
	FORCEINLINE class UStaticMeshComponent* GetBall() const { return Ball; }
	/** Returns SpringArm subobject **/
	FORCEINLINE class USpringArmComponent* GetSpringArm() const { return SpringArm; }
	/** Returns Camera subobject **/
	FORCEINLINE class UCameraComponent* GetCamera() const { return Camera; }
};
