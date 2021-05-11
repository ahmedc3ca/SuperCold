using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private double[] timeStamps = {1.515102,    1.515102,
                            3.221769,    3.221769,
                            3.564263,    3.564263,
                            6.362268,    6.362268, 
                            7.070476,    7.070476,    
                            8.835193,    8.835193,    
                            9.531791,    9.531791,    
                            12.736145,   12.736145,   
                            13.374694,   13.374694,   
                            14.106122,   14.106122,   
                            15.476100,   15.476100,   
                            16.277188,   16.277188,   
                            16.985397,   16.985397,   
                            19.806621,   19.806621,   
                            20.572880,   20.572880,   
                            23.359274,   23.359274,   
                            23.707574,   23.707574,   
                            24.090703,   24.090703,  
                            25.843810,   25.843810,  
                            26.180499,   26.180499,   
                            28.281905,   28.281905,   
                            28.978503,   28.97850};
    public GameObject obstacle; 

    // Start is called before the first frame update
    void Start()
    {
        //distance = vitesse * temps
        for(int i = 0; i < timeStamps.Length; i++)
        {
            Instantiate(obstacle, new Vector3(0, 0.5f, (float)timeStamps[i] * 50f), Quaternion.identity);
        }
    }

}
