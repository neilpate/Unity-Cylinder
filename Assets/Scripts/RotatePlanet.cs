using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    GameObject world;

    public InputActionMap actionMap;
    public InputAction rotateCounterClockwise;
    public InputAction rotateClockwise;
    public InputAction rotateUp;
    public InputAction rotateDown;


    public bool RotatingCounterClockwise;
    public bool RotatingClockwise;
    public bool RotatingUp;
    public bool RotatingDown;



    public bool RotatingClockwiseSecondMethod;

    [SerializeField]
    public float RotateSpeedControl;

    private float rotateClockwiseSpeed;
    private float rotateUpSpeed;
  
    
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        rotateClockwiseSpeed = 0;
    }

    private void Awake()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        rotateCounterClockwise = actionMap.FindAction("RotatePlanetCounterClockwise");
        rotateClockwise = actionMap.FindAction("RotatePlanetClockwise");
        rotateUp = actionMap.FindAction("RotatePlanetUp");
        rotateDown = actionMap.FindAction("RotatePlanetDown");



        //Example, calling delegate without specifying any parameters
        rotateClockwise.performed += OnRotateClockwise;

        //Example, calling delegate while specifying the full parameters. This is identical to the example above
        //ctx stands for "context" and seems to be a standish way of naming the parameter
        rotateCounterClockwise.performed += ctx => OnRotateCounterClockwise(ctx);
        //Example, doing a bit of logic on the parameter and then calling the delegate
        rotateClockwise.performed += ctx => OnRotateClockwiseWithBoolean (ctx.ReadValue<float>() > 0);

        //Example, just doing some logic but not actually calling a delegate
        rotateClockwise.performed += _ => count++;
     
        rotateUp.performed += ctx => OnRotateUp(ctx);
        rotateDown.performed += ctx => OnRotateDown(ctx);
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

    private void OnRotateUp(InputAction.CallbackContext obj)
    {
        RotatingUp = obj.ReadValue<float>() > 0;
    }


    private void OnRotateDown(InputAction.CallbackContext obj)
    {
        RotatingDown = obj.ReadValue<float>() > 0;
    }



    void FixedUpdate()
    {
        if (RotatingClockwise)
        {
            rotateClockwiseSpeed -= RotateSpeedControl;
        }

        if (RotatingCounterClockwise)
        {
            rotateClockwiseSpeed += RotateSpeedControl;
        }

        if (RotatingUp)
        {
            rotateUpSpeed += RotateSpeedControl;
        }

        if (RotatingDown)
        {
            rotateUpSpeed -= RotateSpeedControl;
        }


        var rotation = new Vector3(rotateUpSpeed, 0, rotateClockwiseSpeed);
        
        //Rotate the world gameobject relative to the whole world
        //This is done so that when we try and control the object the motions are
        //sensibly relative to the world. rather than the current orientation of the object
        world.transform.Rotate(rotation,Space.World);

    }
}
