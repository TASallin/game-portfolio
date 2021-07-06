using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    private bool on = false;
    public Material onMaterial;
    public Material offMaterial;
    public GameObject MineBody;
    public string soundy;

    private void Start()
    {
    }
    void Awake(){
    }

    void OnTriggerEnter(Collider c) {
        if (!on && c.attachedRigidbody != null) {
            if (c.attachedRigidbody.gameObject.GetComponent<PlayerHealthAgent>() != null) {
                on = !on;
                Animator a = GetComponent<Animator>();
                a.Play("MineActive");
                //source.PlayOneShot(soundy);
                Audio.Play(soundy);
                MineBody.GetComponent<Renderer>().material.color = onMaterial.color;
            }
        }
    }

    void OnTriggerExit(Collider c) {
        if (on && c.attachedRigidbody != null) {
            if (c.attachedRigidbody.gameObject.GetComponent<PlayerHealthAgent>() != null) {
                on = !on;
                Animator a = GetComponent<Animator>();
                a.Play("Idle");
                Audio.Pause(soundy);
                MineBody.GetComponent<Renderer>().material.color = offMaterial.color;
            }
        }
    }
}
