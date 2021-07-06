using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 velocity;
    public float invincibility = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibility > 0) {
            invincibility -= Time.deltaTime;
        }
        //transform.Translate(velocity*Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity*Time.deltaTime);
    }

    void OnTriggerEnter(Collider c) {
       if (!c.isTrigger && invincibility <= 0) {
           Destroy(gameObject);
       }
    }
}
