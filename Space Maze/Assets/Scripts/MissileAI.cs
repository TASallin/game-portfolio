using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAI : MonoBehaviour
{
    private Vector3 target;
    public float maxAngularSpeed;
    public float maxSpeed;
    public GameObject player;
    
    private Rigidbody rb;
    private AIState state;
    private float lifeSpan;
    private float attackTimer;

    public string soundy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        rb.maxAngularVelocity = maxAngularSpeed;      
        state = AIState.Chase;
        lifeSpan = 10;
        attackTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case AIState.Asleep:
                return;
                break;
            case AIState.Chase:
                lifeSpan -= Time.deltaTime;
                if (lifeSpan < 0) {
                    state = AIState.Attack;
                    rb.AddForce(rb.velocity*-1, ForceMode.VelocityChange);
                } else {
                    Chase();
                }
                break;
            case AIState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }

    public void Chase() {
        Vector3 playerPos = player.transform.position;
        target = playerPos;
        float angle = Vector3.Angle(target - transform.position, rb.velocity);
        if (rb.velocity.magnitude < maxSpeed && angle < 45 || rb.velocity.magnitude < 0.5f) {
            rb.AddForce(transform.forward*600*Time.deltaTime);
        } else {
            rb.AddForce(rb.velocity*-600*Time.deltaTime);
        }
        if (angle > 5) {
            Vector3 targetDir = Vector3.Normalize(target - transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(0,  Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg, 0));
        }
    }

    public void Attack() {
        if (attackTimer > 0) {
            Vector3 playerPos = player.transform.position;
            target = playerPos;
            Vector3 targetDir = Vector3.Normalize(target - transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(0,  Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg, 0));
            attackTimer -= Time.deltaTime;
        } else {
            if (rb.velocity.magnitude < maxSpeed*3) {
                rb.AddForce(transform.forward*900*Time.deltaTime);
            }
        }
    }

    /*
    void OnTriggerEnter(Collider c) {
        // if (c.gameObject.GetComponent<PlayerController>()) {
        //     Debug.Log("Lost to a missile KEKW");

        //     PlayerController p = c.gameObject.GetComponent<PlayerController>();
        //     p.TakeDamage(damage);
        //     Die();
        if (state == AIState.Attack) {
            Die();
        }
    }
    */

    void Die() {
        state = AIState.Asleep;
        Audio.Play(soundy);
        Destroy(gameObject);
    }

    public AIState GetState() {
        return state;
    }

}
