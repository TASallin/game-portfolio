using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private ButtonManager[] buttons;
    private BossTurretAI boss;
    public GameObject room;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        if (room == null) {
            try {
            room = transform.parent.parent.gameObject;
            } catch {
                return;
            }
        }
        buttons = room.GetComponentsInChildren<ButtonManager>();
        boss = room.GetComponentInChildren<BossTurretAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!open) {
            if (boss == null) {
                open = true;
            }
            if (buttons == null && boss == null) {
                Open();
                return;
            }
            foreach (ButtonManager button in buttons) {
                if (!button.on) {
                    open = false;
                    break;
                }
            }
            if (open) {
                Open();
            }
        }
    }

    void Open() {
        if (buttons.Length > 0) {
            GameState.GetGame().score += 50*GameState.GetGame().level;
            Audio.Play("Door");
        }
        //gameObject.SetActive(false);
        transform.localScale = new Vector3(0.2f, 1, 1);
        if (transform.parent.rotation.eulerAngles.z == 270) {
            transform.Translate(new Vector3(-4, 0, 0));
        } else {
            transform.Translate(new Vector3(4, 0, 0));
        }
    }
}