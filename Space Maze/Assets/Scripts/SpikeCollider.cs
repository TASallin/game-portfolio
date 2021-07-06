using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    public float damage = 20;

    private void Start()
    {
    }

    /*
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<PlayerController>()) {
            Debug.Log("Player Collision with spike");

            PlayerController player = c.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            source.Play();
        }
    }
    void OnTriggerExit(Collider c)
    {
        Debug.Log("No longer colliding with spike");
        source.Pause();
    }
    */
}
