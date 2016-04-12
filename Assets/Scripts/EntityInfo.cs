using UnityEngine;
using System.Collections;
using System;


class AnimationTimer
{
    float timeLeft;
    float totalAnimationTime;
    float startHitBoxHeight;
    float startHitBoxY;
    float endHitBoxHeight;
    float endHitBoxY;
    float currentHitBoxHeight;
    float currentHitBoxY;

    public AnimationTimer(float animTime, float startHeight, float endHeight, float startY, float endY)
    {
        timeLeft = 0f;
        totalAnimationTime = animTime;
        startHitBoxHeight = startHeight;
        startHitBoxY = startY;
        endHitBoxHeight = endHeight;
        endHitBoxY = endY;
    }

    public void Update(float elapsed)
    {
        if (timeLeft > elapsed)
        {
            timeLeft -= elapsed;
        } else
        {
            timeLeft = 0f;
        }

        adjustHitBox();
    }

    void adjustHitBox()
    {
        float fraction = timeLeft / totalAnimationTime;
        currentHitBoxHeight = Mathf.Lerp(startHitBoxHeight, endHitBoxHeight, 1 - fraction);
        currentHitBoxY = Mathf.Lerp(startHitBoxY, endHitBoxY, 1 - fraction);
    }

    public void startTimer()
    {
        timeLeft = totalAnimationTime;
    }

    public bool isFinished()
    {
        return (timeLeft <= 0f);
    }

    public float getHeight()
    {
        return currentHitBoxHeight;
    }

    public float getY()
    {
        return currentHitBoxY;
    }


}

public class EntityInfo : MonoBehaviour {

    CharacterController characterController;

    AnimationTimer crouchToStandTimer;
    AnimationTimer standToCrouchTimer;
    float crouchAnimTime = 0.2f;
    float crouchHeightOffset = 0.5f;
    float crouchYOffset = 0.22f;

    AnimationTimer jumpTimer;
    AnimationTimer landTimer;
    float jumpAnimTime = 0.2f;
    float landedAnimTime = 0.5f;
    float jumpHeightOffset = 0.5f;
    float jumpYOffset = 0.0f;
    

    public float startAcceleration;
    public float stopAcceleration;
    public float topSpeed;
    public float gravity;
    public float topFallSpeed;
    public float jumpSpeed;
    int state;
    int facing;
    bool inAir;

    Vector3 movementVector;

    public int PLAYTESTING;
    public bool FREEFORM;

    void setPhysics()
    {
        switch (PLAYTESTING)
        {
            case 0:
            default:
                //gravity = -0.15f;
                //jumpSpeed = 0.3f;
                //topFallSpeed = Mathf.Abs(gravity) * 4;

                //                startAcceleration = 0.2f;
                //              stopAcceleration = 0.5f;
                //            topSpeed = 0.2f;
                //          break;
                gravity = -0.5f;
                jumpSpeed = 0.3f;
                topFallSpeed = Mathf.Abs(gravity) * 4;

                startAcceleration = 0.2f;
                stopAcceleration = 0.5f;
                topSpeed = 0.2f;
                break;
            case 1:
                gravity = -0.15f;
                jumpSpeed = 0.1f;
                topFallSpeed = Mathf.Abs(gravity) * 4;

                startAcceleration = 0.1f;
                stopAcceleration = 0.3f;
                topSpeed = 0.15f;
                break;
            case 2:
                gravity = -0.15f;
                jumpSpeed = 0.2f;
                topFallSpeed = Mathf.Abs(gravity) * 4;

                startAcceleration = 0.5f;
                stopAcceleration = 0.5f;
                topSpeed = 0.1f;
                break;

            case 3:
                gravity = -0.15f;
                jumpSpeed = 0.15f;
                topFallSpeed = Mathf.Abs(gravity) * 4;

                startAcceleration = 0.2f;
                stopAcceleration = 0.2f;
                topSpeed = 0.07f;
                break;


        }
    }

    // Use this for initialization
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        //PLAYTESTING = 0;
        FREEFORM = false;


        crouchToStandTimer = new AnimationTimer(crouchAnimTime, characterController.height - crouchHeightOffset, characterController.height,
            characterController.center.y - crouchYOffset, characterController.center.y);

        standToCrouchTimer = new AnimationTimer(crouchAnimTime, characterController.height, characterController.height - crouchHeightOffset,
            characterController.center.y, characterController.center.y - crouchYOffset);

        jumpTimer = new AnimationTimer(jumpAnimTime, characterController.height, characterController.height - jumpHeightOffset,
            characterController.center.y, characterController.center.y - jumpYOffset);

        landTimer = new AnimationTimer(landedAnimTime, characterController.height - jumpHeightOffset, characterController.height,
            characterController.center.y - jumpYOffset, characterController.center.y);

        

        state = 0;  // idle       = 0
                    // move right = 1
                    // move left  = 2

        facing = 1; // face right = 1
                    // face left = -1

        setPhysics();
        movementVector = Vector3.zero;
        inAir = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!FREEFORM)
        {
            setPhysics();
        }
        
        switch (state)
        {
            case 3: // Crouching
                stopHorizontalMovement();
                break;
            case 0: // Idle
                stopHorizontalMovement();
                break;
            case 1: // Move right
                if (crouchToStandTimer.isFinished())
                {
                    movementVector += transform.TransformDirection(Vector3.forward) * startAcceleration * facing * Time.deltaTime;
                }

                break;
            case 2: // Move left
                if (crouchToStandTimer.isFinished())
                {
                    movementVector += transform.TransformDirection(Vector3.back) * startAcceleration * facing * Time.deltaTime;
                }

                break;
        }

        // Crouch timer
        if (state != 3 && !crouchToStandTimer.isFinished())
        {
            crouchToStandTimer.Update(Time.deltaTime);
            characterController.height = crouchToStandTimer.getHeight();
            Vector3 tempVector = characterController.center;
            tempVector.y = crouchToStandTimer.getY();
            characterController.center = tempVector;

        } else if (state == 3 && !standToCrouchTimer.isFinished())
        {
            standToCrouchTimer.Update(Time.deltaTime);
            characterController.height = standToCrouchTimer.getHeight();
            Vector3 tempVector = characterController.center;
            tempVector.y = standToCrouchTimer.getY();
            characterController.center = tempVector;

        }

        // Jumping
        if (!jumpTimer.isFinished())
        {
            jumpTimer.Update(Time.deltaTime);
            characterController.height = jumpTimer.getHeight();
            Vector3 tempVector = characterController.center;
            tempVector.y = jumpTimer.getY();
            characterController.center = tempVector;
            if (jumpTimer.isFinished())
            {
                inAir = true;
                movementVector.y = jumpSpeed;
            }
        }

        // Landing
        if (!landTimer.isFinished())
        {
            landTimer.Update(Time.deltaTime);
            characterController.height = landTimer.getHeight();
            Vector3 tempVector = characterController.center;
            tempVector.y = landTimer.getY();
            characterController.center = tempVector;

        }

        // Gravity
        if (!characterController.isGrounded || inAir)
        {
            movementVector += transform.TransformDirection(Vector3.up) * gravity * Time.deltaTime;
        }
        else 
        {
            movementVector.y = gravity;
        }

        // Clamp
        movementVector.z = Mathf.Clamp(movementVector.z, -topSpeed, topSpeed);
        movementVector.y = Mathf.Clamp(movementVector.y, -topFallSpeed, topFallSpeed);
    }

    void stopHorizontalMovement()
    {
        if (Math.Abs(movementVector.z) < stopAcceleration * Time.deltaTime)
        {
            movementVector.z = 0;
        }
        else if (movementVector.z > 0)
        {
            movementVector += transform.TransformDirection(Vector3.back) * stopAcceleration * facing * Time.deltaTime;
        }
        else
        {
            movementVector += transform.TransformDirection(Vector3.forward) * stopAcceleration * facing * Time.deltaTime;
        }
    }

    
    internal void setState(int s)
    {
        


        if (state != s)
        {
            if (s == 3)
            {
                if (!isJumping())
                standToCrouchTimer.startTimer();
                
            }
            else if (state == 3)
            {
                crouchToStandTimer.startTimer();

            }

            state = s;
        }

    }
    
    internal Vector3 getCurrentMovementVector()
    {
        return movementVector;
    }
    
    internal float getStartAcceleration()
    {
        return startAcceleration;
    }

    internal bool isCrouching()
    {
        return (state == 3);
    }

    internal bool isJumping()
    {
        return inAir || !jumpTimer.isFinished();
    }

    internal void jump()
    {
        if (state != 3 && characterController.isGrounded && !inAir && jumpTimer.isFinished() && landTimer.isFinished() && crouchToStandTimer.isFinished())
        {
            jumpTimer.startTimer();

        }
    }

    internal void landed()
    {
        if (inAir)
        {
            inAir = false;
            landTimer.startTimer();
        }
    }

    internal void setFacing(int f)
    {
        if (facing != f)
        {
            facing = f;
            transform.Rotate(Vector3.up * 180);
        }
    }

    internal int getFacing()
    {
        return facing;
    }

}
