using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAgent : HealthAgent
{
    private PlayerState player;
    private Animator animator;
    private Rigidbody rb;
    private IEnumerator damageCoroutine;
    public GameObject shield;
    private GameObject shieldRef;

    public float damageBufferTime = 2f;
    void Start() {
        player = GameState.GetGame().player;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        Vector3 pTransform = this.transform.position;

        shieldRef = Instantiate(
            shield,
            new Vector3(pTransform.x, pTransform.y + 1.5f, pTransform.z),
            Quaternion.identity,
            this.transform
        );
        shieldRef.SetActive(false);
    }

    void Update()
    {
        if (iframes > 0) {
            iframes -= Time.deltaTime;
        }

        if (player.shields > 0) {
            shieldRef.SetActive(true);
        } else {
            shieldRef.SetActive(false);
        }
    }

    public override void Damage(int power) {
        if (iframes <= 0) {
            if (player.shields > 0) {
                player.shields -= 1;
                Debug.Log("Shielded!");
            } else {
                int damage = System.Math.Max((int)(power*GameState.GetGame().damageMult) - defense, 1);
                player.health = System.Math.Max(player.health - damage, 0);
                Debug.Log("Took damage, new health is " + player.health.ToString());
            }
            if (player.health <= 0) Debug.Log("Player died");

            iframes = iframeMax;
        }
    }

    public IEnumerator TakingDamage(float damage) {
        player.isTakingDamage = true;
        animator.SetBool("IsTakingDamage", player.isTakingDamage);

        yield return new WaitForSeconds(damageBufferTime);

        player.isTakingDamage = false;
        animator.SetBool("IsTakingDamage", player.isTakingDamage);
    }

    public override void Die() {
    }
}


