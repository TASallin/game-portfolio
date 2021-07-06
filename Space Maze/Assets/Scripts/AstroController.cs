using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 8f;
    public int health = 100;
    private bool isGrounded;

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 0.8f;


    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public bool Action
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        }
        /*
        if (Input.GetKey("w") && Vector3.Angle(Vector3.forward, transform.forward) < 45 || Input.GetKey("a") && Vector3.Angle(Vector3.left, transform.forward) < 45 || Input.GetKey("s") && Vector3.Angle(Vector3.back, transform.forward) < 45 || Input.GetKey("d") && Vector3.Angle(Vector3.right, transform.forward) < 45) {
            v = 1f;
        } else {
            v = 0;
        }
        
        if (Input.GetKey("w") && Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up) < -25 || Input.GetKey("a") && Vector3.SignedAngle(Vector3.left, transform.forward, Vector3.up) < -25 || Input.GetKey("s") && Vector3.SignedAngle(Vector3.back, transform.forward, Vector3.up) < -25 || Input.GetKey("d") && Vector3.SignedAngle(Vector3.right, transform.forward, Vector3.up) < -25) {
            h = 2;
        } else if (Input.GetKey("w") && Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up) > 25 || Input.GetKey("a") && Vector3.SignedAngle(Vector3.left, transform.forward, Vector3.up) > 25 || Input.GetKey("s") && Vector3.SignedAngle(Vector3.back, transform.forward, Vector3.up) > 25 || Input.GetKey("d") && Vector3.SignedAngle(Vector3.right, transform.forward, Vector3.up) > 25) {
            h = -2;
        } else {
            h = 0;
        }
        */

        Vector3 inputAngle;
        if (Input.GetKey("w") && Input.GetKey("d")) {
            inputAngle = Vector3.Normalize(new Vector3(1, 0, 1));
        } else if (Input.GetKey("d") && Input.GetKey("s")) {
            inputAngle = Vector3.Normalize(new Vector3(1, 0, -1));
        } else if (Input.GetKey("s") && Input.GetKey("a")) {
            inputAngle = Vector3.Normalize(new Vector3(-1, 0, -1));
        } else if (Input.GetKey("a") && Input.GetKey("w")) {
            inputAngle = Vector3.Normalize(new Vector3(-1, 0, 1));
        } else if (Input.GetKey("w")) {
            inputAngle = Vector3.forward;
        } else if (Input.GetKey("a")) {
            inputAngle = Vector3.left;
        } else if (Input.GetKey("s")) {
            inputAngle = Vector3.back;
        } else if (Input.GetKey("d")) {
            inputAngle = Vector3.right;
        } else {
            inputAngle = new Vector3(0, 0, 0);
        }
        if (inputAngle.magnitude > 0) {
            transform.forward = inputAngle;
        }

        if (Vector3.SignedAngle(inputAngle, transform.forward, Vector3.up) < -25 && inputAngle.magnitude > 0) {
            h = 5;
        } else if (Vector3.SignedAngle(inputAngle, transform.forward, Vector3.up) > 25 && inputAngle.magnitude > 0) {
            h = -5;
        } else {
            h = 0;
        } 

        if (Vector3.Angle(inputAngle, transform.forward) < 45 && inputAngle.magnitude > 0) {
            v = 0.8f;
        } else {
            v = 0;
        }

        //Vector3 mov = new Vector3(h, 0.0f, v);
        //rb.AddForce(mov * speed);

        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        // {
        //     rb.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
        //     isGrounded = false;
        // }
        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;

        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
            Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;

        Action = Input.GetButtonDown("Fire1");
        Jump = Input.GetButtonDown("Jump");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Speed Upgrade"))
        {
            other.gameObject.SetActive(false);
            speed = 12f;
        }
        else if (other.gameObject.CompareTag("Health Upgrade"))
        {
            other.gameObject.SetActive(false);

            int newHealth = health + 10;

            this.transform.localScale = new Vector3(2, 2, 2);
            Debug.Log("Health changed from " + health.ToString() + " to " + newHealth.ToString());
            health = newHealth;
        }
    }
}
