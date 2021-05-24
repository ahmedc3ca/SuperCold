using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lengthController : MonoBehaviour
{
    public float start;
    public float end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, start);
        transform.localScale = new Vector3(1f, 1f, (end - start) / 10f);
    }

    void setter(float start, float end)
    {
        this.start = start;
        this.end = end;
    }
}
