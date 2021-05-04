using System;
using System.Collections;
using UnityEngine;

public class ElectronMover : MonoBehaviour
{
    //Electron Physics
    private Rigidbody rb;
    public float electronAcceleration = 300f;
    public float electronJumpPower = 10f;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * Time.deltaTime * electronAcceleration;
    }

    private void Start() {

        //Initialize temperature logic
        currentTemperature = 0;
        temperatureSlider.SetMaxTemperature(MaxTemperature);
        temperatureSlider.SetTemperature(0);
        StartCoroutine("IncrementTemperature");
    }
    // Update is called once per frame
    void Update()
    {
        //Move electron depending on input
        if (Input.GetKeyDown("space") && !isSuperCold)
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
            currentTemperature += 0.4f;
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
        rb.velocity += Vector3.up * electronJumpPower;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "obstacle"){
            ResetElectron();
            //rb.velocity = Vector3.forward * Time.deltaTime * electronAcceleration;
            currentTemperature = Math.Max(0, currentTemperature - 20);
        }
    }

    private void ResetElectron(){
            this.transform.position = GameObject.Find("spawnPoint").transform.position;
            rb.velocity = Vector3.forward * Time.deltaTime * electronAcceleration;
    }
}
