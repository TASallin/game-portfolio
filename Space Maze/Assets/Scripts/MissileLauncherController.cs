using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour
{
    private GameObject missile;
    public GameObject missilePrefab;
    public float warning = 3f;
    public float interval = 5f;
    public AIState state;
    public Transform fake;
    public GameObject player;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        state = AIState.Prepare;
        timer = warning;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0) {
            timer -= Time.deltaTime;
        }
        if (timer <= warning - GameState.GetGame().damageMult && state == AIState.Asleep) {
            Prep();
        } else if (timer <= 0 && state == AIState.Prepare) {
            Attack();
        } else if (missile == null && state == AIState.Attack) {
            Sleep();
        }
    }

    private void Prep() {
        state = AIState.Prepare;
        fake.localPosition = new Vector3(0, 0, 1);
    }

    private void Attack() {
        state = AIState.Attack;
        missile = Instantiate(missilePrefab, transform.position + new Vector3(0, 0, 1.5f), transform.rotation, transform.parent);
        missile.GetComponent<MissileAI>().player = player;
        missile.GetComponent<Agent>().retain = true;
        Audio.Play("Missile");
        fake.localPosition = new Vector3(0, 0, 0);
    }

    private void Sleep() {
        state = AIState.Asleep;
        timer = interval;
    }
}
