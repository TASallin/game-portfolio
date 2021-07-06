using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderManager : MonoBehaviour
{
    public FloorGenerator roomHolder;
    public GameObject loadingScreen;

    public AudioClip soundy;
    public AudioSource source;

    private void Start()
    {
        GetComponent<AudioSource>().clip = soundy;
        source.clip = soundy;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerHealthAgent>()) {
            source.PlayOneShot(soundy);
            loadingScreen.SetActive(true);
            GameState.GetGame().level += 1;
            GameState.GetGame().damageMult += 0.2f;
            GameState.GetGame().healthMult += 0.2f;
            if (GameState.GetGame().level <= 5) {
                roomHolder.Clean();
            } else {
                SceneManager.LoadScene("win");
            }
        }
    }
}
