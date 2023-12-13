using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private JoystickController joystickController;
    private CharacterController characterController;
    Vector3 moveVector;

    [Header("Settings")]
    [SerializeField] private int moveSpeed;

    private float gravity = -9.81f;
    private float gravityMultipler = 3f;
    private float gravityVelocity;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveVector = joystickController.GetMovePosition() * moveSpeed * Time.deltaTime / Screen.width;

        //Burada karakterin yukar� gitmesini yani u�mas�n� engelliyoruz. Y ekseni yukar� :D
        moveVector.z = moveVector.y;
        moveVector.y = 0;

        playerAnimator.ManageAnitamions(moveVector);

        ApplyGravity();
        characterController.Move(moveVector);
    }

    //Herhangi bir objenin �zerine ��kt���nda tekrar inmesini sa�lad�k.
    private void ApplyGravity()
    {
        if(characterController.isGrounded && gravityVelocity < 0.0f)
        {
            gravityVelocity = -1f;
        }
        else
        {
            gravityVelocity += gravity * gravityMultipler * Time.deltaTime;
        }

        moveVector.y = gravityVelocity; 
    }
}
