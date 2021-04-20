using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMover : MonoBehaviour
{

    public float electronAcceleration = 200f;
    public float electronJumpPower = 300f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * Time.deltaTime * electronAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.velocity += Vector3.up * Time.deltaTime * electronJumpPower;
        }
    }
}
