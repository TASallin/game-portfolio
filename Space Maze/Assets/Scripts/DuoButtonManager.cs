using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoButtonManager : MonoBehaviour
{
    public DuoButtonManager pair;

    public bool on = false;
    public bool pressed = false;
    public Material onMaterial;
    public string clicky;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        pressed = true;
        Audio.Play(clicky);
        if (!on && pair.pressed) {
            on = true;
            gameObject.GetComponent<Renderer>().material.color = onMaterial.color;
            pair.On();
        }
    }

    void OnTriggerExit() {
        pressed = false;
    }

    public void On() {
        on = true;
        gameObject.GetComponent<Renderer>().material.color = onMaterial.color;
    }
}
