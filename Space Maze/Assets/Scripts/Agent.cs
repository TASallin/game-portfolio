using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A class applied to anything with AI or movement that would be problematic when in another room
public class Agent : MonoBehaviour
{

    public bool retain = false;
    public string sound;
    public bool permanent = true;
    // Start is called before the first frame update
    void Start()
    {
        if (!retain) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InRoom() {
        gameObject.SetActive(true);
        if (sound.Length > 0) {
            Audio.Play(sound);
        }
    }

    public virtual void LeaveRoom() {
        if (permanent) {
            gameObject.SetActive(false);
        } else {
            Destroy(gameObject);
        }
    }

    public virtual void SpawnHere() {
        retain = true;
        gameObject.SetActive(true);
    }
}
