using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurretAI : MonoBehaviour
{
    public GameObject hatch;
    public GameObject missile;
    public GameObject lasers;
    public GameObject bullet;
    public List<GameObject> targets;
    public GameObject player;
    public Transform rotates;
    public List<Color> colors;
    public MeshRenderer head;

    private int laserCount;
    private int missileCount;
    private int straightCount;
    private int spinCount;
    private System.Random rng;
    private float timer;
    private float spinning;
    private float shooting;
    private float lazing;
    private float shotTimer;
    private float rotationSpeed;
    private int reverse;

    public float shotInterval = 0.2f;
    public float spinSpeed = 40;

    // Start is called before the first frame update
    void Start()
    {
        missileCount = 0;
        laserCount = 1;
        straightCount = 1;
        spinCount = 1;
        timer = 2;
        spinning = 0;
        lazing = 0;
        shooting = 0;
        rotationSpeed = 1;
        reverse = 1;
        rng = new System.Random();
        head.material.color = colors[GameState.GetGame().level - 1];
    }

    // Update is called once per frame
    void Update()
    {
        bool dead = true;
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i] != null) {
                dead = false;
                break;
            }
        }
        if (dead) {
            Die();
        }

        if (timer <= 0) {
            //spinning = false;
            //shooting = false;
            //lasers.SetActive(false);

            int hat = rng.Next(missileCount + laserCount + straightCount + spinCount);
            if (hat < missileCount) {
                missileCount = -1;
                Missile();
            } else if (hat < missileCount + laserCount) {
                laserCount = -1;
                Laser();
            } else if (hat < missileCount + laserCount + straightCount) {
                straightCount = -1;
                ShootStraight();
            } else {
                spinCount = -1;
                ShootSpin();
            }
            missileCount += 1;
            laserCount += 1;
            straightCount += 1;
            spinCount += 1;
            timer = 5 - 0.8f*GameState.GetGame().level;
            if (GameState.GetGame().level > 2 && rng.Next(3) == 0) {
                reverse = -1*reverse;
            }
            if (GameState.GetGame().level > 1) {
                rotationSpeed = (float)rng.NextDouble() + 1;
            }
        } else {
            timer -= Time.deltaTime;
        }

        if (shooting > 0) {
            shooting -= Time.deltaTime;
            if (shotTimer <= 0) {
                Audio.Play("Gun");
                if (spinning > 0) {
                    GameObject b = Instantiate(bullet, transform.position + 2*rotates.forward + new Vector3(0, 3, 0), Quaternion.identity, transform.parent);
                    b.GetComponent<BulletController>().velocity = 10*rotates.forward;
                    b = Instantiate(bullet, transform.position - 2*rotates.forward + new Vector3(0, 3, 0), Quaternion.identity, transform.parent);
                    b.GetComponent<BulletController>().velocity = -10*rotates.forward;
                    b = Instantiate(bullet, transform.position + 2 * new Vector3(rotates.forward.z, 1.5f, -1*rotates.forward.x), Quaternion.identity, transform.parent);
                    b.GetComponent<BulletController>().velocity = 10*new Vector3(rotates.forward.z, 0, -1*rotates.forward.x);
                    b = Instantiate(bullet, transform.position + -2 * new Vector3(rotates.forward.z, -1.5f, -1*rotates.forward.x), Quaternion.identity, transform.parent);
                    b.GetComponent<BulletController>().velocity = -10*new Vector3(rotates.forward.z, 0, -1*rotates.forward.x);
                } else {
                    Vector3 bullPos = Vector3.MoveTowards(transform.position, player.transform.position, 2);
                    GameObject b = Instantiate(bullet, new Vector3(bullPos.x, 3, bullPos.z), Quaternion.identity, transform.parent);
                    Vector3 velocity = player.transform.position - b.transform.position;
                    Vector3 bullV = 10*GameState.GetGame().damageMult*Vector3.Normalize(Vector3.Normalize(new Vector3(velocity.x, 0, velocity.z)) + new Vector3((float)rng.NextDouble()/2-0.25f, 0, (float)rng.NextDouble()/2-0.25f));
                    b.GetComponent<BulletController>().velocity = bullV;
                }
                shotTimer = shotInterval;
            } else {
                shotTimer -= Time.deltaTime;
            }
        }

        if (spinning > 0) {
            //if (gameObject.GetComponent<PathAgent>() == null) {
                rotates.Rotate(new Vector3(0, spinSpeed * rotationSpeed * reverse * Time.deltaTime, 0));
            //}
            spinning -= Time.deltaTime;
        }

        if (lazing <= 0) {
            lasers.SetActive(false);
        } else {
            lazing -= Time.deltaTime;
        }
    }

    private void Laser() {
        Audio.Play("Laser");
        lasers.SetActive(true);
        spinning = 2;
        lazing = 2;
    }

    private void Missile() {
        Vector3 bullPos = Vector3.MoveTowards(transform.position, player.transform.position, 2);
        GameObject m = Instantiate(missile, new Vector3(bullPos.x, transform.position.y + 2, bullPos.z), Quaternion.identity, transform.parent);
        m.GetComponent<MissileAI>().player = player;
        m.GetComponent<Agent>().retain = true;
        Audio.Play("Missile");
    }

    private void ShootStraight() {
        shooting = 2;
    }

    private void ShootSpin() {
        spinning = 2;
        shooting = 2;
    }

    public void Die() {
        hatch.SetActive(true);
        Destroy(gameObject);
    }
}
