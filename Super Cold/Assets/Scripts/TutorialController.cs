using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject electron;
    public GameObject camera;
    public GameObject[] menus = new GameObject[6];
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<AudioSource>().Pause();
        index = 0;
        for(int i = 1; i < 6; i++)
        {
            menus[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!electron.GetComponent<ElectronMover>().isShowingCanvas)
        {
            return;
        }
        if (Input.GetKeyDown("space"))
        {
            if (index == 5)
            {
                menus[index].SetActive(false);
                electron.GetComponent<ElectronMover>().StartGame();
            }
            else
            {
                menus[index].SetActive(false);
                menus[index + 1].SetActive(true);
                index++;
            }
        }
    }
}
