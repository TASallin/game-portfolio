using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{   

    public AudioSource[] tracks;
    public AudioSource boss;
    private int floor;

    // Start is called before the first frame update
    void Start()
    {
        floor = 1;
    }

    // Update is called once per frame
    void Update()
    {
        try {
        if (floor != GameState.GetGame().level) {
            boss.Stop();
            tracks[floor-1].Stop();
            floor = GameState.GetGame().level;
            tracks[floor-1].Play();
        }
        } catch {
            
        }
    }

    public void Boss() {
        tracks[floor-1].Stop();
        boss.Play();
    }
}
