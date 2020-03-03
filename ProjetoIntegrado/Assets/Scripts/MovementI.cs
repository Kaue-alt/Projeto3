using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementI : MonoBehaviour
{
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotation;
    public float desiredRotationSpeed;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    private Vector3 moveVector;
    private float verticalVel;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }


    void Update()
    {
        InputMagnitude();

        //Somente se precisar que o personagem fique no chão
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 2;
        }

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
        //
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (blockRotation == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }

    }

    void InputMagnitude()
    {
        //Calcula os Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2);
        anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2);

        //Cacula o Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Mover o player fisicamente
        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        }
    }
    

}
