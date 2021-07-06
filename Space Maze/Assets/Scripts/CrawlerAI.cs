using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAI : MonoBehaviour
{
    private Vector3 target;
    public float maxAngularSpeed;
    public float maxSpeed;
    public GameObject player;
    public float sightRadius;
    public GameObject sword;
    public GameObject bullet;
    public float swingRadius = 8;
    public float attackInterval = 3;
    
    private Rigidbody rb;
    private AIState state;
    private float wanderTime;
    private float attackTimer;
    private int damage = 30;
    private System.Random rng;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        rb.maxAngularVelocity = maxAngularSpeed;      
        state = AIState.Chase;
        wanderTime = 3;
        attackTimer = 1;
        rng = new System.Random();
        cooldown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case AIState.Wander:
                if (sword != null && cooldown <= 0 && (player.transform.position - transform.position).magnitude < swingRadius) {
                    state = AIState.Attack;
                    cooldown = 0.5f;
                    sword.GetComponent<BoxCollider>().size = new Vector3(4, 1, 1);
                } else if (bullet != null && cooldown <= 0) {
                    cooldown = attackInterval - 0.5f*(GameState.GetGame().level - 1);
                    GameObject bull = Instantiate(bullet, transform.position + new Vector3(0, 2, 0) + 3*transform.forward, Quaternion.identity, transform.parent);
                    bull.GetComponent<BulletController>().velocity = transform.forward*10*GameState.GetGame().damageMult;
                    bull = Instantiate(bullet, transform.position + new Vector3(0, 2, 0) + 2*transform.forward + 2*transform.right, Quaternion.identity, transform.parent);
                    bull.GetComponent<BulletController>().velocity = Vector3.Normalize(transform.right + transform.forward)*10*GameState.GetGame().damageMult;
                    bull = Instantiate(bullet, transform.position + new Vector3(0, 2, 0) + 2*transform.forward - 2*transform.right, Quaternion.identity, transform.parent);
                    bull.GetComponent<BulletController>().velocity = Vector3.Normalize(transform.forward - transform.right)*10*GameState.GetGame().damageMult;
                    Audio.Play("Gun");
                } else {
                    wanderTime -= Time.deltaTime;
                    if (wanderTime < 0) {
                        state = AIState.Chase;
                        rb.AddForce(rb.velocity*-1, ForceMode.VelocityChange);
                    } else {
                        cooldown -= Time.deltaTime;
                        Wander();
                    }
                }
                break;
            case AIState.Chase:
                if (sword != null && cooldown <= 0 && (player.transform.position - transform.position).magnitude < swingRadius) {
                    state = AIState.Attack;
                    cooldown = 0.5f;
                    sword.GetComponent<BoxCollider>().size = new Vector3(4, 1, 1);
                } else if (bullet != null && cooldown <= 0) {
                    cooldown = attackInterval - 0.5f*(GameState.GetGame().level - 1);
                    GameObject bull = Instantiate(bullet, transform.position + new Vector3(0, 1, 0) + 2.2f*transform.forward, Quaternion.identity, transform.parent);
                    bull.GetComponent<BulletController>().velocity = transform.forward*10*GameState.GetGame().damageMult;
                    bull = Instantiate(bullet, transform.position + new Vector3(0, 1, 0) + 1.6f*transform.forward + 1.6f*transform.right, Quaternion.identity, transform.parent);
                    bull.GetComponent<BulletController>().velocity = Vector3.Normalize(transform.right + transform.forward)*10*GameState.GetGame().damageMult;
                    bull = Instantiate(bullet, transform.position + new Vector3(0, 1, 0) + 1.6f*transform.forward - 1.6f*transform.right, Quaternion.identity, transform.parent);
                    bull.GetComponent<BulletController>().velocity = Vector3.Normalize(transform.forward - transform.right)*10*GameState.GetGame().damageMult;
                    Audio.Play("Gun");
                } else {
                    cooldown -= Time.deltaTime;
                    Chase();
                }
                break;
            case AIState.Attack:
                Attack();
                cooldown -= Time.deltaTime;
                if (cooldown <= 0) {
                    state = AIState.Chase;
                    cooldown = attackInterval - 0.5f*(GameState.GetGame().level - 1);
                    sword.GetComponent<BoxCollider>().size = new Vector3(2, 1, 1);
                }
                break;
            default:
                break;
        }
    }

    public void Chase() {
        Vector3 playerPos = player.transform.position;
        target = playerPos;
        float angle = Vector3.Angle(target - transform.position, rb.velocity);
        if (rb.velocity.magnitude < maxSpeed*GameState.GetGame().damageMult) {
            rb.AddForce(transform.forward*900*Time.deltaTime);
        }
        if (angle > 5) {
            Vector3 targetDir = Vector3.Normalize(target - transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(0,  Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg, 0));
        }
    }

    public void Wander() {
        float dAngle = rng.Next(21) - 10;
        transform.Rotate(new Vector3(0, dAngle*5*Time.deltaTime, 0));
        if (rb.velocity.magnitude <  maxSpeed*GameState.GetGame().damageMult) {
            rb.AddForce(transform.forward*900*Time.deltaTime);
        }
    }

    public void Attack() {
        transform.Rotate(new Vector3(0, 360*Time.deltaTime/0.5f, 0));
    }

    void OnTriggerEnter(Collider c) {
        if (rb == null) {
            return;
        }
        if (!c.isTrigger && rb.velocity.magnitude < maxSpeed/4f && state == AIState.Chase) {
            state = AIState.Wander;
            rb.AddForce(rb.velocity*-2, ForceMode.VelocityChange);
            transform.Rotate(new Vector3(0, 180, 0));
            wanderTime = 3;
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
