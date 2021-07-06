using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlasterController : MonoBehaviour
{   
    public GameObject laser;
    public float warning = 2f;
    public float interval = 4f;
    public float duration = 2f;
    public AIState state;
    public Material onMat;
    public Material offMat;
    public MeshRenderer barrel;
    public float offset = 4f;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        state = AIState.Asleep;
        timer = offset;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= warning - GameState.GetGame().damageMult && state == AIState.Asleep) {
            Prep();
        } else if (timer <= 0 && state == AIState.Prepare) {
            Attack();
        } else if (timer <= 0 && state == AIState.Attack) {
            Sleep();
        }
    }

    private void Prep() {
        state = AIState.Prepare;
        barrel.material = onMat;
    }

    private void Attack() {
        state = AIState.Attack;
        laser.SetActive(true);
        Audio.Play("Laser");
        timer = duration;
    }

    private void Sleep() {
        state = AIState.Asleep;
        laser.SetActive(false);
        barrel.material = offMat;
        timer = interval;
    }
}
