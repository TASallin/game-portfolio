using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checker : MonoBehaviour
{
    public AudioClip soundy;
    public AudioSource source;
    public GameObject door;

    void Start()
    {
        GetComponent<AudioSource>().clip = soundy;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform iChild in transform)
        {
            if (iChild.tag == "Door")
            {
                GameObject dooryy = iChild.gameObject;
                if (dooryy.activeInHierarchy == false)
                {
                    source.PlayOneShot(soundy);
                    soundy = null;
                    //Debug.LogError("Hi");
                }
            }
        }
    }
}
