using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private IEnumerator damageCoroutine;
    private Rigidbody rb;
    private PlayerState player;
    public GameObject shield;
    private bool isGrounded;
    public float damageBufferTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // disable shield upon start
        shield.transform.parent = this.transform;
        player = GameState.GetGame().player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 mov = new Vector3(h, 0.0f, v);
        rb.AddForce(mov * player.speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            rb.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        // check if shieldUp bool has been set, if shield collider
        // not active, then set active
        //if (player.shieldUp && !shield.activeInHierarchy) shield.SetActive(true);
        //else if (!player.shieldUp && shield.activeInHierarchy) shield.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }
    public void TakeDamage(float damage) {
        /*
        if (!player.shieldUp) {
            player.health -= damage;
            Debug.Log("Took damage, new health is " + player.health.ToString());
            if (player.health <= 0) Debug.Log("Player died");

            damageCoroutine = TakingDamage(damage);
            StartCoroutine(damageCoroutine);
        } else {
            Debug.Log("Shield took the hit!");
            player.shieldUp = false;
        }
        */
    } 

    public IEnumerator TakingDamage(float damage) {
        player.isTakingDamage = true;
        animator.SetBool("IsTakingDamage", player.isTakingDamage);

        yield return new WaitForSeconds(damageBufferTime);

        player.isTakingDamage = false;
        animator.SetBool("IsTakingDamage", player.isTakingDamage);
    }
    
}
