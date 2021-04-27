using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject electron;
    public float distanceFromElectron = 10;
    public float cameraHeight = 3;


    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(this.transform.position.x,this.transform.position.y,electron.transform.position.z - distanceFromElectron);
        this.transform.position = temp;
    }
}
