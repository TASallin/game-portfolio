using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{   
    public static Audio jukebox;

    // Start is called before the first frame update
    void Start()
    {
        jukebox = gameObject.GetComponent<Audio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public static void Play(string name) {
		jukebox.gameObject.transform.Find(name).gameObject.GetComponent<AudioSource>().Play();
	}

  public static void Pause(string name) {
	  jukebox.gameObject.transform.Find(name).gameObject.GetComponent<AudioSource>().Pause();
  }

  public static void Stop() {
    foreach (Transform t in jukebox.gameObject.transform) {
      t.gameObject.GetComponent<AudioSource>().Stop();
    }
  }
}
