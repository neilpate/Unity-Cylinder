using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    GameObject world;

    public InputActionMap actionMap;
    public InputAction rotateCounterClockwise;
    public InputAction rotateClockwise;

    public bool RotatingCounterClockwise;
    public bool RotatingClockwise;
    public bool RotatingClockwiseSecondMethod;

    private float rotateSpeed;
    [SerializeField]
    public float RotateSpeedControl;

    public int count;

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 0;
    }

    private void Awake()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        rotateCounterClockwise = actionMap.FindAction("RotateCounterClockwise");
        rotateClockwise = actionMap.FindAction("RotateClockwise");

        //Example, calling delegate without specifying any parameters
        rotateClockwise.performed += OnRotateClockwise;

        //Example, calling delegate while specifying the full parameters. This is identical to the example above
        //ctx stands for "context" and seems to be a standish way of naming the parameter
        rotateCounterClockwise.performed += ctx => OnRotateCounterClockwise(ctx);

        //Example, doing a bit of logic on the parameter and then calling the delegate
        rotateClockwise.performed += ctx => OnRotateClockwiseWithBoolean (ctx.ReadValue<float>() > 0);

        //Example, just doing some logic but not actually calling a delegate
        rotateClockwise.performed += _ => count++;
    }

    //Note, if the delegate is called the same as the action in the action map 
    //then Unity will try and invoke it automatically if the Player Input behaviour is set to Send Messages
    //This will cause an exception as the method will have a different signature!

    private void OnRotateClockwiseWithBoolean(bool v)
    {
        RotatingClockwiseSecondMethod = v;
    }



    private void OnRotateClockwise(InputAction.CallbackContext obj)
    {
        RotatingClockwise = obj.ReadValue<float>() > 0;
    }


    private void OnRotateCounterClockwise(InputAction.CallbackContext obj)
    {
        RotatingCounterClockwise = obj.ReadValue<float>() > 0;
    }



    void FixedUpdate()
    {
        if (RotatingClockwise)
        {
            rotateSpeed -= RotateSpeedControl;
        }

        if (RotatingCounterClockwise)
        {
            rotateSpeed += RotateSpeedControl;
        }

        world.transform.Rotate(0, 0, rotateSpeed);



    }
}
