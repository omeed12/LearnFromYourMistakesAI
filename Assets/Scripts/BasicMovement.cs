using UnityEngine;
using System.Collections;
using System;

public class BasicMovement : MonoBehaviour {

    Animator animator;
    EntityInfo entityInfo;
    CharacterController characterController;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        entityInfo = gameObject.GetComponent<EntityInfo>();
        characterController = gameObject.GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 movementVector = entityInfo.getCurrentMovementVector();

        characterController.Move(movementVector * gameObject.transform.localScale.magnitude);

        animator.SetFloat("MovementSpeed", characterController.velocity.z * entityInfo.getFacing());
        animator.SetFloat("VerticalSpeed", characterController.velocity.y);
        animator.SetBool("IsGrounded", characterController.isGrounded);
        animator.SetBool("IsCrouching", entityInfo.isCrouching());
        animator.SetBool("IsJumping", entityInfo.isJumping());
        animator.SetInteger("Facing", entityInfo.getFacing());

        if (characterController.isGrounded)
        {
            entityInfo.landed();
        }

    }
}
