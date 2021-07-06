using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatEngine : MonoBehaviour
{
    public Transform roomHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            GameState.GetGame().player.health = 999999;
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            GameState.GetGame().level = 5;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            foreach (Transform t in roomHolder) {
                foreach (Transform d in t) {
                    if (d.gameObject.tag == "Door") {
                        d.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
