using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAudio : MonoBehaviour
{

    public AudioClip soundy;
    public AudioSource source;

    void Start()
    {
        GetComponent<AudioSource>().clip = soundy;
        source.clip = soundy;
    }

    void Update()
    {
        
    }
}
