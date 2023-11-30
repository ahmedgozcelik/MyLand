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
        characterController.Move(moveVector);
    }
}
