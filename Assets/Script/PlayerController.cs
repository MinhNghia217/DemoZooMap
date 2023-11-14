using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement; 
    bool isMovementPressed;
    bool isRunPressed;

    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 5.0f;
    int zero = 0;

    //gravity variables
    float gravity = -9.8f;
    float groundedGravity = -.05f;

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 4.0f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpingAnimating = false;


    public  Transform orientation;
    public float speed;
    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
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
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpingAnimating = true;
            isJumping = true;
            float previousYVelocity = currentMovement.y;
            float newYVelociy = currentMovement.y + initialJumpVelocity;
            float nextYVelocity = (previousYVelocity + newYVelociy) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;

            

        } else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    private void handleRotation()
    {
        /*Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation= transform.rotation;
        
        if (isMovementPressed )
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }*/


        // L?y góc xoay c?a camera



        Quaternion cameraRotation = Quaternion.Euler(0f, orientation.rotation.eulerAngles.y, 0f); ;
        

        // Ch? xoay khi có s? di chuy?n
        if (isMovementPressed)
        {
            // Xác ??nh h??ng di chuy?n d?a trên góc xoay c?a camera
            Vector3 moveDirection = cameraRotation * new Vector3(currentMovement.x, 0.0f, currentMovement.z);

            // Xác ??nh h??ng nhìn d?a trên h??ng di chuy?n
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Xoay nhân v?t theo h??ng nhìn m?i
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;

        //Debug.Log(currentMovement.x + "/" + currentMovement.z);

        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != zero || currentMovementInput.y != zero;
    }

    private void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking) 
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }        

    private void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if(characterController.isGrounded)
        {
            if(isJumpingAnimating)
            {
                animator.SetBool("isJumping", false);
                isJumpingAnimating = false;
            }
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelociy = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelociy) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelociy = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelociy) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
    }
    private void Update()
    {
        handleRotation();   
        handleAnimation();
        /* if(isRunPressed)
         {
             characterController.Move(currentRunMovement * Time.deltaTime);
            // Debug.Log("move-1");
         } else
         {
            // Debug.Log("move-2");
             characterController.Move(currentMovement * Time.deltaTime);    
         }*/



        /* Vector3 moveDirection = isRunPressed ? currentRunMovement : currentMovement;
         Vector3 transformedDirection = transform.TransformDirection(moveDirection);*/

        if (isRunPressed)
        {
            Vector3 moveDirection_temp = orientation.right * currentRunMovement.x + orientation.forward * currentRunMovement.z;
            Vector3 moveDirection = new Vector3(moveDirection_temp.x, currentRunMovement.y, moveDirection_temp.z);
            Debug.Log(moveDirection);
            characterController.Move(moveDirection * Time.deltaTime* speed);
        }
        else
        {
            Vector3 moveDirection_temp = orientation.right * currentMovement.x + orientation.forward * currentMovement.z;
            Vector3 moveDirection = new Vector3(moveDirection_temp.x, currentMovement.y, moveDirection_temp.z);
            Debug.Log(moveDirection);
            characterController.Move(moveDirection * Time.deltaTime* speed);
        }
       

        


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