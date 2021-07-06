using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int power = 10;
    public string hitSound;
    public bool friendlyFire = true;

    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c) {
        HealthAgent a;
        if (friendlyFire) {
            a = c.gameObject.GetComponent<HealthAgent>();
        } else {
            a = c.gameObject.GetComponent<PlayerHealthAgent>();
        }
        if (a != null) {
            if (hitSound.Length > 0) {
                Audio.Play(hitSound);
            }
            a.Damage(power);
        }
    }
}
