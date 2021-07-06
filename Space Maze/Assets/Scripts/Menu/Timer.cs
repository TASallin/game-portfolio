using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float startTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = (int)(Time.time-startTime);
        GetComponent<Text>().text = Format(seconds);
    }

    public string Format(int rawTime) {
        int minutes = rawTime/60;
        int seconds = rawTime%60;
        string mid = ":";
        if (seconds < 10) {
            mid = ":0";
        }
        string first = "";
        if (minutes < 10) {
            first = "0";
        }
        return first + minutes + mid + seconds;
    }
}
