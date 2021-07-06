using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAgent : MonoBehaviour
{

    public int health = 10;
    public int defense = 0;
    public float iframeMax = 0.5f;
    public int score  = 0;

    protected float iframes;

    // Start is called before the first frame update
    void Start()
    {
        health = (int)(health*GameState.GetGame().healthMult);
    }

    // Update is called once per frame
    void Update()
    {
        if (iframes > 0) {
            iframes -= Time.deltaTime;
        }
    }

    public virtual void Damage(int power) {
        if (iframes <= 0) {
            int damage = System.Math.Max(power - defense, 1);
            health = System.Math.Max(health - damage, 0);
            if (health == 0) {
                Die();
            }
            iframes = iframeMax;
        }
    }

    public virtual void Die() {
        GameState.GetGame().score += score*GameState.GetGame().level;
        Destroy(gameObject);
    }
}
