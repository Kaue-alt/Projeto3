using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
   
    public float walkSpeed = 2;
    public float runSpeed = 6;
    
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;



    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;
    float gravity = -12;
    float jumpHeight = 1;
    float airControlPercent;
    bool Crouch=false;
    

    Animator animator;
    Transform cameraT;
    CharacterController controller;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {  //inputs
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);
        bool Crouch = Input.GetKey(KeyCode.LeftControl);

        Move(inputDir, running);

        AnimatorControl(Crouch, running);

        Crouching(Crouch);
    }

    void Move(Vector2 inputDir,bool running)
    {
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

       
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
            
        }

        
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }
        if (airControlPercent == 0) 
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    void AnimatorControl(bool Crouch, bool running)
    {
        //Animator
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        animator.SetBool("Crouch", Crouch);
    }

    void Crouching(bool Crouch)
    {
        if (Crouch)
        {
            controller.center = new Vector3(0, 0.4f, 0);
            controller.height = 0.7f;
        }
        else
        {
            controller.center = new Vector3(0, 0.7f, 0);
            controller.height = 1.4f;
        }
    }

}
