using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    public HackedRobotController bot;
    public PlayerController player;
    public GameObject text;
    public Material onColor;
    public Material offColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.E)) {
            // if (other.gameObject.GetComponent<HackedRobotController>() && player.disabled)
            // {
            //     player.Toggle();
            //     bot.Toggle();
            // }
            if (other.gameObject.GetComponent<PlayerHealthAgent>())
            {
                bot.Toggle();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerHealthAgent>()) {
            //text.SetActive(true);
            bot.Toggle();
            if (!bot.disabled) {
                gameObject.GetComponent<MeshRenderer>().material = onColor;
            } else {
                gameObject.GetComponent<MeshRenderer>().material = offColor;
            }
        }
    }

    /*
    void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<PlayerHealthAgent>()) {
            text.SetActive(false);
        }
    }
    */
}
