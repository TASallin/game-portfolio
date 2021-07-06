using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public GameObject model;
    public float interval = 2f;
    public float cooldown;
    public float rotationSpeed;
    System.Random rng;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = interval;
        rng = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        } else {
            Shoot();
            cooldown = (interval - 0.2f*(GameState.GetGame().level));
        }
        float angle = Vector3.Angle(player.transform.position, model.transform.forward);
        if (angle > 5) {
            Vector3 targetDir = Vector3.Normalize(player.transform.position - model.transform.position);
            model.transform.rotation = Quaternion.Euler(new Vector3(0,  Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg, 0));
            //model.transform.rotation = Quaternion.Euler(0, Vector3.RotateTowards(model.transform.rotation.eulerAngles, targetDir, rotationSpeed*Time.deltaTime, 999).y, 0);
            //Debug.Log(Vector3.RotateTowards(model.transform.forward, targetDir, rotationSpeed*Time.deltaTime, 999));
            //Debug.Log(model.transform.forward);
        }
    }

    public void Shoot() {
        Audio.Play("Gun");
        Vector3 bullPos = Vector3.MoveTowards(transform.position, player.transform.position, 2);
        GameObject b = Instantiate(bullet, new Vector3(bullPos.x, 2 + transform.position.y, bullPos.z), Quaternion.identity, transform.parent);
        Vector3 velocity = player.transform.position - b.transform.position;
        Vector3 bullV = 10*GameState.GetGame().damageMult*Vector3.Normalize(Vector3.Normalize(new Vector3(velocity.x, 0, velocity.z)) + new Vector3((float)rng.NextDouble()/2-0.25f, 0, (float)rng.NextDouble()/2-0.25f));
        b.GetComponent<BulletController>().velocity = bullV;
    }
}
