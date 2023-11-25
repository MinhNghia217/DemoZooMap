using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    public CharacterController characterController;
    public Animator animator;


    int isRunningHash;
    int isJumpingHash;


    Vector2 currentMovementInput;
    Vector3 currentRunMovement;

    bool isMovementPressed;


    float rotationFactorPerFrame = 15.0f;
 

    //gravity variables
    float gravity = -9.8f;
    float groundedGravity = -.05f;

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 4.0f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    
    bool isJumpingAnimating = false;


    public  Transform orientation;
    public float speed;
    public float jump;
    private void Awake()
    {
        playerInput = new PlayerInput();
      
      


        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;


        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariables();



        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = ( maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpingAnimating = true;
            isJumping = true;

            currentRunMovement.y = jump;

        } else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    private void handleRotation()
    {
        Quaternion cameraRotation = Quaternion.Euler(0f, orientation.rotation.eulerAngles.y, 0f); ;
        

        if (isMovementPressed)
        {
            // Xác ??nh h??ng di chuy?n d?a trên góc xoay c?a camera
            Vector3 moveDirection = cameraRotation * new Vector3(currentRunMovement.x, 0.0f, currentRunMovement.z);

            // Xác ??nh h??ng nhìn d?a trên h??ng di chuy?n
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Xoay nhân v?t theo h??ng nhìn m?i
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentRunMovement.x = currentMovementInput.x ;
        currentRunMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        
    }



    private void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void handleAnimation()
    {

        if (isMovementPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if (!isMovementPressed)
        {
            animator.SetBool(isRunningHash, false);
        }

       
    }        

    private void handleGravity()
    {
        bool isFalling = currentRunMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if(characterController.isGrounded)
        {
            
            if (isJumpingAnimating)
            {
                animator.SetBool("isJumping", false);
                isJumpingAnimating = false;
            }
            currentRunMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
           
            float previousYVelocity = currentRunMovement.y;
            float newYVelociy = currentRunMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelociy) * .5f;

            currentRunMovement.y = nextYVelocity;
        }
        else
        {
          
            float previousYVelocity = currentRunMovement.y;
            float newYVelociy = currentRunMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelociy) * .5f;

            currentRunMovement.y = nextYVelocity;
        }
    }
    private void handleMove()
    {
        
        Vector3 moveDirection_temp = orientation.right * currentRunMovement.x + orientation.forward *currentRunMovement.z;
        Vector3 moveDirection = new Vector3(moveDirection_temp.x * speed, currentRunMovement.y, moveDirection_temp.z * speed);

        

        characterController.Move(moveDirection * Time.deltaTime);
        
        
       
    }
    private void Update()
    {
        handleRotation();   
        handleAnimation();

        handleMove();


        handleGravity();
        handleJump();
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}