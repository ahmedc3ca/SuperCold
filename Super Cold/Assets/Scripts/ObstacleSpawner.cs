using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private Vector2[] timeStamps = { new Vector2(4.179592f, 1f), new Vector2(6.968889f, -1f), new Vector2(12.649070f, 1f), new Vector2(18.361179f, -1f), new Vector2(24.026848f, 1f), new Vector2(26.134059f, -1f), new Vector2(29.675102f, 1f), new Vector2(35.201451f, -1f), new Vector2(36.617868f, 1f), new Vector2(38.045896f, -1f), new Vector2(40.901950f, 1f), new Vector2(43.084626f, -1f), new Vector2(46.590839f, 1f), new Vector2(48.645805f, 1f), new Vector2(49.342404f, 1f), new Vector2(52.152018f, -1f), new Vector2(53.614875f, -1f), new Vector2(57.852517f, -1f), new Vector2(60.650522f, 1f), new Vector2(63.483356f, -1f), new Vector2(69.125805f, 1f), new Vector2(71.970249f, -1f), new Vector2(74.733424f, 1f), new Vector2(76.103401f, 1f), new Vector2(80.410703f, -1f), new Vector2(81.165351f, -1f), new Vector2(81.861950f, -1f), new Vector2(83.278367f, 1f), new Vector2(84.729615f, 1f), new Vector2(86.111202f, 1f), new Vector2(87.457959f, -1f), new Vector2(88.862766f, -1f), new Vector2(91.718821f, -1f), new Vector2(97.523810f, 1f), new Vector2(103.050159f, -1f), new Vector2(108.657778f, 1f), new Vector2(109.400816f, 1f), new Vector2(110.120635f, 1f), new Vector2(114.381497f, -1f), new Vector2(115.101315f, -1f), new Vector2(115.786304f, -1f), new Vector2(117.179501f, 1f), new Vector2(118.561088f, -1f), new Vector2(119.954286f, 1f), new Vector2(120.616054f, -1f), new Vector2(121.324263f, 1f), new Vector2(125.689615f, 1f), new Vector2(126.362993f, 1f), new Vector2(127.024762f, 1f), new Vector2(128.464399f, 1f), new Vector2(129.962086f, -1f), new Vector2(132.632381f, 1f), new Vector2(134.095238f, -1f), new Vector2(134.466757f, 1f), new Vector2(134.861497f, -1f), new Vector2(138.112290f, 1f) };
    public GameObject obstacle;
    public GameObject wall1;

    // Start is called before the first frame update
    void Start()
    {
        //distance = vitesse * temps
        for(int i = 0; i < timeStamps.Length; i++)
        {
            Instantiate(obstacle, new Vector3(0, 0.5f, timeStamps[i].x * 50f), Quaternion.identity);
        }
    }

}
