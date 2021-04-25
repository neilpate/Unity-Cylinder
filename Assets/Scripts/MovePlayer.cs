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

    public InputActionMap actionMap;
    public InputAction move;

    //public bool RotatingCounterClockwise;
    //public bool RotatingClockwise;
    //public bool RotatingUp;
    //public bool RotatingDown;



    //public bool RotatingClockwiseSecondMethod;

    [SerializeField]
    public float MoveSpeedControl;

    //private float rotateClockwiseSpeed;
    //private float rotateUpSpeed;


    public int count;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        move = actionMap.FindAction("Move");


        //Example, calling delegate without specifying any parameters
        move.started += OnMove;

    }


    private void OnMove(InputAction.CallbackContext obj)
    {
        var movement = obj.ReadValue<Vector2>();

        var translation = new Vector3(movement[0] * MoveSpeedControl, movement[1] * MoveSpeedControl, 0);

        count++;
        //   player.transform.Translate(translation);


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
