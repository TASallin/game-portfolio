using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public GameObject laser;
    public bool on = false;
    public Material onMaterial;
    public Material offMaterial;

    public string soundy;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle() {
        if (laser.activeSelf) {
            laser.SetActive(false);
        } else {
            laser.SetActive(true);
        }
    }

    void OnTriggerEnter() {
        on = !on;
        Audio.Play(soundy);
        if (on) {
            gameObject.GetComponent<Renderer>().material.color = onMaterial.color;
        } else {
            gameObject.GetComponent<Renderer>().material.color = offMaterial.color;
        }
        Toggle();
    }
}
