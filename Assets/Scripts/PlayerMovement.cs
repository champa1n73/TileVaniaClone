using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float climbSpeed = 10f;
    private bool isTryingToClimb;
    private Vector2 moveInput;
    
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private CapsuleCollider2D myCapsuleCollider;
    private float gravityScaleAtStart;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    private void Update()
    {
        Run();
        FlipSrpite();
        ClimbLadder();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && (IsOnGround() || IsOnLadder() ))
        {
            isTryingToClimb = false;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);     
        }
    }

    private void Run()
    {
        myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        if(MathF.Abs(moveInput.x) > Mathf.Epsilon && !IsOnLadder())
        {
            isTryingToClimb = false;
        }
    }
    private void FlipSrpite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

    }

    private void ClimbLadder()
    {
        // Check if Player is trying to climb (Holding or Pressing Up Arrow?)
        if(MathF.Abs(moveInput.y) > Mathf.Epsilon) 
        {
            isTryingToClimb = true;
        }
        if (IsOnLadder() && isTryingToClimb) // Is Player attaching to the ladder?
        {
                myRigidbody.gravityScale = 0;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
                
                bool isClimbingOnLadder = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
                if (isClimbingOnLadder)
                {
                    myAnimator.SetBool("isClimbing", true);
                }
                else
                {
                    myAnimator.SetBool("isClimbing", false);
                }
        }
        else
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
        }
    }

    private bool IsOnGround()
    {
        return myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsOnLadder()
    {
        return myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
}
