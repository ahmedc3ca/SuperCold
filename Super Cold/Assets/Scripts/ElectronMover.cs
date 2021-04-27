using System;
using System.Collections;
using UnityEngine;

public class ElectronMover : MonoBehaviour
{

    public float electronAcceleration = 300f;
    public float electronJumpPower = 10f;

    public int MaxTemperature = 100;
    public int currentTemperature;
    public TemperatureSlider temperatureSlider;
    private Rigidbody rb;

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
        if (Input.GetKeyDown("space"))
        {
            rb.velocity += Vector3.up * electronJumpPower;
        }
        
        temperatureSlider.SetTemperature(currentTemperature);
    }

    IEnumerator IncrementTemperature() {
        for(;;) {
            currentTemperature += 1;
            yield return new WaitForSeconds(.05f);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "obstacle"){
            ResetElectron();
            currentTemperature = Math.Max(0, currentTemperature - 20);
        }
    }

    private void ResetElectron(){
            this.transform.position = GameObject.Find("spawnPoint").transform.position;
            rb.velocity = Vector3.forward * Time.deltaTime * electronAcceleration;
    }
}
