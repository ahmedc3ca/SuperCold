﻿using System;
using System.Collections;
using UnityEngine;

public class ElectronMover : MonoBehaviour
{
    //Audio
    public GameObject camera;

    //Electron Physics
    private Rigidbody rb;
    public float electronVelocity = 50f;
    public float electronJumpPower = 30f;
    private bool isJumping = false;
    public float jumpDrag = 0f;

    //Temperature Logic
    public float MaxTemperature = 100;
    public float Tmax = 75;
    public float currentTemperature;
    public TemperatureSlider temperatureSlider;

    //SuperCold Movement
    public float rotationAngle = -90f;
    public float rotationWidth = 4f;
    public float rotationHeight = 7f;

    //GameState
    public bool isSuperCold = false;
    public bool hasLost = false;

    //Menus Logic
    public bool isShowingCanvas = true;
    private void Awake()
    {

    }

    private void Start() {

        isShowingCanvas = true;
        Physics.gravity = new Vector3(0, -100, 0);

    }

    public void StartGame()
    {

        camera.GetComponent<AudioSource>().Play();
        isShowingCanvas = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * electronVelocity;
        rb.drag = 0f;
        //Initialize temperature logic
        currentTemperature = 20;
        temperatureSlider.SetMaxTemperature(MaxTemperature);
        temperatureSlider.SetTemperature(0);
        StartCoroutine("IncrementTemperature");
    }
    // Update is called once per frame
    void Update()
    {
        if (isShowingCanvas)
        {

            return;
        }
        //Move electron depending on input
        if (Input.GetKeyDown("space") && !isSuperCold && !isJumping)
        {
            Jump();
        }
        if(Input.GetKey(KeyCode.RightArrow) && isSuperCold)
        {
            RotateRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow) && isSuperCold)
        {
            RotateLeft();
        }
        //Update UI slider 
        temperatureSlider.SetTemperature(currentTemperature);
        //Detect GameState
        UpdateGameState();
    }

    private void RotateRight()
    {
        rotationAngle += 0.03f;
        RecalculateRotation();
    }

    private void RotateLeft()
    {
        rotationAngle -= 0.03f;
        RecalculateRotation();
    }

    private void RecalculateRotation()
    {
        float x = (float)Math.Cos(rotationAngle) * rotationWidth;
        float y = (float)Math.Sin(rotationAngle) * rotationHeight + 11f;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    private void UpdateGameState()
    {
        if (currentTemperature > Tmax)
        {
            if (!isSuperCold)
            {
                EnterSuperCold();
            }
        }
        else
        {
            if (isSuperCold)
            {
                LeaveSuperCold();
            }
        }


        if (currentTemperature == 0)
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        //TODO
    }

    IEnumerator IncrementTemperature() {
        for(;;) {
            currentTemperature += 0.1f;
            yield return new WaitForSeconds(.02f);
        }
    }

    private void EnterSuperCold()
    {
        transform.position += Vector3.up * 10f;
        rb.useGravity = false;
        //rb.velocity = Vector3.zero;
        isSuperCold = true;
    }

    private void LeaveSuperCold()
    {
        transform.position -= Vector3.up * 10f;
        rb.useGravity = true;
        isSuperCold = false;
    }

    private void Jump()
    {
        isJumping = true;
        rb.drag = jumpDrag;
        rb.useGravity = true;
        rb.velocity += Vector3.up * electronJumpPower;
        StartCoroutine("StopGravity");

    }

    IEnumerator StopGravity()
    {
        yield return new WaitForSeconds(0.6f);
        rb.useGravity = false;
        rb.drag = 0f;
        rb.velocity = Vector3.forward * electronVelocity;
        isJumping = false;
    }


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "obstacle"){
            ResetElectron(other);

        }
    }

    private void ResetElectron(Collision other){
            this.transform.position = other.gameObject.transform.position + Vector3.forward * 1.5f;
            rb.velocity = Vector3.forward * electronVelocity;
            currentTemperature = Math.Max(0, currentTemperature - 10);
    }
}
