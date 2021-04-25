using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{

    GravityAttractor gravityAttractor;
    Rigidbody rb;

    
    // Start is called before the first frame update
    void Start()
    {
        gravityAttractor = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rb = GetComponent<Rigidbody>();
    
    }

    private void FixedUpdate()
    {
        gravityAttractor.Attract(transform, rb);
    }

}
