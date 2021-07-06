using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackedRobotController : MonoBehaviour
{
    private Rigidbody rb;
    public bool disabled = true;
    public float speed = 8f;
    public float maxSpeed = 2f;
    public float toggleTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggleTimer > 0) {
            toggleTimer -= Time.deltaTime;
        }
        
        if (disabled) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 mov = new Vector3(h, 0.0f, v);
        rb.AddForce(mov * speed * Time.deltaTime, ForceMode.Impulse);
        
    }

    public void Toggle() {
        if (toggleTimer <= 0) {
            disabled = !disabled;
            toggleTimer = 0.2f;
            Debug.Log(this.disabled ? "Robot is disabled" : "Robot is enabled");
            Audio.Play("Missile");
        }
    }
}
