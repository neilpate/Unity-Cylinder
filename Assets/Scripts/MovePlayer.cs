using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    GameObject player;

    [SerializeField]
    public float MoveSpeedControl;

    [SerializeField]
    public float JumpForce;

    InputActionMap actionMap;
    InputAction move;
    InputAction jump;

    Rigidbody rb;

    private void Awake()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        move = actionMap.FindAction("Move");
        jump = actionMap.FindAction("Jump");

        move.started += OnMove;
        jump.started += OnJump;


        rb = player.GetComponent<Rigidbody>();

    }


    private void OnJump(InputAction.CallbackContext obj)
    {
        rb.AddForce(0, JumpForce, 0);

    }



    private void OnMove(InputAction.CallbackContext obj)
    {
        var movement = obj.ReadValue<Vector2>();

        var translation = new Vector3(movement[0] * MoveSpeedControl, movement[1] * MoveSpeedControl, 0);


        if (movement[0] < 0)
        {
            player.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, MoveSpeedControl);
        }

        if (movement[0] > 0)
        {
            player.transform.RotateAround(new Vector3(0, 0, 0), Vector3.down, MoveSpeedControl);
        }

        if (movement[1] < 0)
        {
            player.transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, MoveSpeedControl);
        }

        if (movement[1] > 0)
        {
            player.transform.RotateAround(new Vector3(0, 0, 0), Vector3.right, MoveSpeedControl);
        }



        //       RotatingClockwise = obj.ReadValue<float>() > 0;
    }


}
