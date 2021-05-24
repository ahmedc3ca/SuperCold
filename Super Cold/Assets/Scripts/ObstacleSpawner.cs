using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private Vector2[] timeStamps = { new Vector2(4.179592f, 1f), new Vector2(6.968889f, -1f), new Vector2(12.649070f, 1f), new Vector2(18.361179f, -1f), new Vector2(24.026848f, 1f), new Vector2(26.134059f, -1f), new Vector2(29.675102f, 1f), new Vector2(35.201451f, -1f), new Vector2(36.617868f, 1f), new Vector2(38.045896f, -1f), new Vector2(40.901950f, 1f), new Vector2(43.084626f, -1f), new Vector2(46.590839f, 1f), new Vector2(48.645805f, 1f), new Vector2(49.342404f, 1f), new Vector2(52.152018f, -1f), new Vector2(53.614875f, -1f), new Vector2(57.852517f, -1f), new Vector2(60.650522f, 1f), new Vector2(63.483356f, -1f), new Vector2(69.125805f, 1f), new Vector2(71.970249f, -1f), new Vector2(74.733424f, 1f), new Vector2(76.103401f, 1f), new Vector2(80.410703f, -1f), new Vector2(81.165351f, -1f), new Vector2(81.861950f, -1f), new Vector2(83.278367f, 1f), new Vector2(84.729615f, 1f), new Vector2(86.111202f, 1f), new Vector2(87.457959f, -1f), new Vector2(88.862766f, -1f), new Vector2(91.718821f, -1f), new Vector2(97.523810f, 1f), new Vector2(103.050159f, -1f), new Vector2(108.657778f, 1f), new Vector2(109.400816f, 1f), new Vector2(110.120635f, 1f), new Vector2(114.381497f, -1f), new Vector2(115.101315f, -1f), new Vector2(115.786304f, -1f), new Vector2(117.179501f, 1f), new Vector2(118.561088f, -1f), new Vector2(119.954286f, 1f), new Vector2(120.616054f, -1f), new Vector2(121.324263f, 1f), new Vector2(125.689615f, 1f), new Vector2(126.362993f, 1f), new Vector2(127.024762f, 1f), new Vector2(128.464399f, 1f), new Vector2(129.962086f, -1f), new Vector2(132.632381f, 1f), new Vector2(134.095238f, -1f), new Vector2(134.466757f, 1f), new Vector2(134.861497f, -1f), new Vector2(138.112290f, 1f) };
    public GameObject obstacle;
    public GameObject[] walls = new GameObject[8];
    public int index = 0;
    private float[] xvalues = {0f, 8.5f, 12f, 8.5f, 0f, -8.5f, -12f, -8.5f};
    private float[] yvalues = {0f, 3.5f, 12f, 20.5f, 24f, 20.5f, 12f, 3.5f};
    public GameObject electron;
    public GameObject floor;
    private List<GameObject> spawnedSC = new List<GameObject>();
    private List<GameObject> spawnedNormal = new List<GameObject>();
    private List<int> indices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //distance = vitesse * temps
        for(int i = 0; i < timeStamps.Length; i++)
        {
            //calculate which prefab to spawn
            index = mod(index + (int)timeStamps[i].y, 8);
            int index2 = index;
            indices.Add(index2);

            //spawn super cold obstacle
            GameObject wall = Instantiate(walls[index], new Vector3(xvalues[index], yvalues[index], timeStamps[i].x * 50f), Quaternion.Euler(0f,0f, index * 45f));
            wall.GetComponent<lengthController>().start = timeStamps[i].x * 50f;
            wall.GetComponent<lengthController>().end = (timeStamps[(i == timeStamps.Length - 1) ? timeStamps.Length - 1 : i + 1].x) * 50f;
            spawnedSC.Add(wall);
            wall.SetActive(false);

            //spawn normal obstacle
            GameObject obst = Instantiate(obstacle, new Vector3(0, 0.5f, timeStamps[i].x * 50f), Quaternion.identity);
            spawnedNormal.Add(obst);
        }
    }


    public void SpawnSuperColdObstacles()
    {
        foreach(GameObject go in spawnedNormal)
        {
            go.SetActive(false);
            floor.SetActive(false);
        }
        foreach (GameObject go in spawnedSC)
        {
            go.SetActive(true);
        }
    }

    public void SpawnNormalObstacles()
    {
        foreach (GameObject go in spawnedSC)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in spawnedNormal)
        {
            go.SetActive(true);
            floor.SetActive(true);
        }
    }



    public bool IsInsideWalls(float z, float angle)
    {
        int index = GetIndexFromZ(z/50f);
        int higherAngle = Mathf.FloorToInt(angle);
        int lowerAngle = Mathf.CeilToInt(angle);
        higherAngle = mod(higherAngle, 8);
        lowerAngle = mod(lowerAngle, 8);
        Debug.Log("higher angle: "+higherAngle);
        Debug.Log("lower angle: "+lowerAngle);
        Debug.Log("index: "+index);
        switch (index)
        {
            case 0:
                return lowerAngle == 5 && higherAngle == 6;
            case 1:
                return lowerAngle == 4 && higherAngle == 5;
            case 2:
                return lowerAngle == 3 && higherAngle == 4;
            case 3:
                return lowerAngle == 2 && higherAngle == 3;
            case 4:
                return lowerAngle == 1 && higherAngle == 2;
            case 5:
                return lowerAngle == 0 && higherAngle == 1;
            case 6:
                return true;
            case 7:
                return lowerAngle == 6 && higherAngle == 7;
            default:
                return false;
        }
    }

    private int GetIndexFromZ(float z)
    {
            if (z < 4.179592f)
            {
                return indices[0];
            }
            else if (z < 6.968889f)
            {
                return indices[1];
            }
            else if (z < 12.649070f)
            {
                return indices[2];
            }
            else if (z < 18.361179f)
            {
                return indices[3];
            }
            else if (z < 24.026848f)
            {
                return indices[4];
            }
            else if (z < 26.134059f)
            {
                return indices[5];
            }
            else if (z < 29.675102f)
            {
                return indices[6];
            }
            else if (z < 35.201451f)
            {
                return indices[7];
            }
            else if (z < 36.617868f)
            {
                return indices[8];
            }
            else if (z < 38.045896f)
            {
                return indices[9];
            }
            else if (z < 40.901950f)
            {
                return indices[10];
            }
            else if (z < 43.084626f)
            {
                return indices[11];
            }
            else if (z < 46.590839f)
            {
                return indices[12];
            }
            else if (z < 48.645805f)
            {
                return indices[13];
            }
            else if (z < 49.342404f)
            {
                return indices[14];
            }
            else if (z < 52.152018f)
            {
                return indices[15];
            }
            else if (z < 53.614875f)
            {
                return indices[16];
            }
            else if (z < 57.852517f)
            {
                return indices[17];
            }
            else if (z < 60.650522f)
            {
                return indices[18];
            }
            else if (z < 63.483356f)
            {
                return indices[19];
            }
            else if (z < 69.125805f)
            {
                return indices[20];
            }
            else if (z < 71.970249f)
            {
                return indices[21];
            }
            else if (z < 74.733424f)
            {
                return indices[22];
            }
            else if (z < 76.103401f)
            {
                return indices[23];
            }
            else if (z < 80.410703f)
            {
                return indices[24];
            }
            else if (z < 81.165351f)
            {
                return indices[25];
            }
            else if (z < 81.861950f)
            {
                return indices[26];
            }
            else if (z < 83.278367f)
            {
                return indices[27];
            }
            else if (z < 84.729615f)
            {
                return indices[28];
            }
            else if (z < 86.111202f)
            {
                return indices[29];
            }
            else if (z < 87.457959f)
            {
                return indices[30];
            }
            else if (z < 88.862766f)
            {
                return indices[31];
            }
            else if (z < 91.718821f)
            {
                return indices[32];
            }
            else if (z < 97.523810f)
            {
                return indices[33];
            }
            else if (z < 103.050159f)
            {
                return indices[34];
            }
            else if (z < 108.657778f)
            {
                return indices[35];
            }
            else if (z < 109.400816f)
            {
                return indices[36];
            }
            else if (z < 110.120635f)
            {
                return indices[37];
            }
            else if (z < 114.381497f)
            {
                return indices[38];
            }
            else if (z < 115.101315f)
            {
                return indices[39];
            }
            else if (z < 115.786304f)
            {
                return indices[40];
            }
            else if (z < 117.179501f)
            {
                return indices[41];
            }
            else if (z < 118.561088f)
            {
                return indices[42];
            }
            else if (z < 119.954286f)
            {
                return indices[43];
            }
            else if (z < 120.616054f)
            {
                return indices[44];
            }
            else if (z < 121.324263f)
            {
                return indices[45];
            }
            else if (z < 125.689615f)
            {
                return indices[46];
            }
            else if (z < 126.362993f)
            {
                return indices[47];
            }
            else if (z < 127.024762f)
            {
                return indices[48];
            }
            else if (z < 128.464399f)
            {
                return indices[49];
            }
            else if (z < 129.962086f)
            {
                return indices[50];
            }
            else if (z < 132.632381f)
            {
                return indices[51];
            }
            else if (z < 134.095238f)
            {
                return indices[52];
            }
            else if (z < 134.466757f)
            {
                return indices[53];
            }
            else if (z < 134.861497f)
            {
                return indices[54];
            }
            else if (z < 138.112290f)
            {
                return indices[55];
            }
            else
            {
                return 0;
            }
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
