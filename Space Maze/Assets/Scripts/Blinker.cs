using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    private float timer;
    public float interval = 1;
    private bool on;
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
        on = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = interval;
            on = !on;
            GetComponent<MeshRenderer>().enabled = on;
        }
    }
}
