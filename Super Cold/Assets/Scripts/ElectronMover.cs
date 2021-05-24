using System;
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
    public bool isGrounded = true;
    public float jumpDrag = 0f;

    //Temperature Logic
    public float MaxTemperature = 100;
    public float Tmin = 25;
    public float currentTemperature;
    public TemperatureSlider temperatureSlider;

    //SuperCold Movement
    public float rotationAngle = -90f;
    public float rotationWidth = 4f;
    public float rotationHeight = 7f;

    //GameState
    public bool isSuperCold = false;
    public bool hasLost = false;
    public bool isInsideWall = false;

    //Menus Logic
    public bool isShowingCanvas = true;

    //Obstacle Manager
    public GameObject obstacleSpawner;

    public GameObject supercoldCanvas;
    public GameObject winCanvas;

    public AudioClip music;
    public AudioClip music_supercold;

    private void Awake()
    {

    }

    private void Start() {
        supercoldCanvas.SetActive(false);
        winCanvas.SetActive(false);
        isShowingCanvas = true;
        Physics.gravity = new Vector3(0, -100, 0);
        isGrounded = true;


    }

    public void StartGame()
    {
        
        
        camera.GetComponent<AudioSource>().Play();
        isShowingCanvas = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * electronVelocity;
        rb.drag = 0f;
        //Initialize temperature logic
        currentTemperature = 99;
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
        if (Input.GetKeyDown("space") && !isSuperCold && isGrounded)
        {
            Jump();
            rb.useGravity = true;
        }
        if(Input.GetKey(KeyCode.RightArrow) && isSuperCold)
        {
            RotateRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow) && isSuperCold)
        {
            RotateLeft();
        }
        //lose points
        if (isSuperCold && !isInsideWall)
        {
            currentTemperature += 0.05f;
        }
        
        //Update UI slider 
        temperatureSlider.SetTemperature(currentTemperature);
        //Detect GameState
        UpdateGameState();
        if(transform.position.z > 7000f)
        {
            WinGame();
        }
    }


    private void WinGame()
    {
        winCanvas.SetActive(true);
        camera.GetComponent<AudioSource>().Stop();
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
        if (currentTemperature < Tmin)
        {
            if (!isSuperCold)
            {
                EnterSuperCold();
            }
        }
        else if (currentTemperature > (Tmin + 10))
        {
            if (isSuperCold)
            {
                LeaveSuperCold();
            }
        }


        if (currentTemperature == 100)
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
            if(currentTemperature > 0f)
            {
                currentTemperature -= 0.07f;
            }
            else
            {
                currentTemperature = 0;
            }

            yield return new WaitForSeconds(.02f);
        }
    }
    IEnumerator ShowSuperColdCanvas()
    {
        supercoldCanvas.SetActive(true);
        RotateRight();
        RotateLeft();
        yield return new WaitForSeconds(1.5f);
        supercoldCanvas.SetActive(false);
    }
    private void EnterSuperCold()
    {

        camera.GetComponent<AudioSource>().clip = music_supercold;
        camera.GetComponent<AudioSource>().time = transform.position.z / 50f;
        camera.GetComponent<AudioSource>().Play();
        transform.position += Vector3.up * 10f;
        rb.useGravity = false;
        //rb.velocity = Vector3.zero;
        isSuperCold = true;
        //StartCoroutine("ShowSuperColdCanvas");
        obstacleSpawner.GetComponent<ObstacleSpawner>().SpawnSuperColdObstacles();
        rb.velocity = Vector3.forward * electronVelocity;
    }

    private void LeaveSuperCold()
    {
        camera.GetComponent<AudioSource>().clip = music;
        camera.GetComponent<AudioSource>().time = transform.position.z / 50f;
        camera.GetComponent<AudioSource>().Play();
        transform.position = new  Vector3(0f, 0.5f, transform.position.z);
        isSuperCold = false;
        obstacleSpawner.GetComponent<ObstacleSpawner>().SpawnNormalObstacles();
        rb.velocity = Vector3.forward * electronVelocity;
    }

    private void Jump()
    {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        isInsideWall = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isInsideWall = true;
    }
    private void OnTriggerExit(Collider other)
    {

        isInsideWall = false;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "obstacle"){
            ResetElectron(other);

        }else if(other.gameObject.tag == "floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "floor")
        {
            Debug.Log("wtff");
            isGrounded = false;
        }
    }

    private void ResetElectron(Collision other){
            this.transform.position = other.gameObject.transform.position + Vector3.forward * 1.80f;
            rb.velocity = Vector3.forward * electronVelocity;
            currentTemperature = Math.Min(100, currentTemperature + 17);
    }
}
