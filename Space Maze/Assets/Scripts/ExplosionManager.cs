using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
  public int damage = 50;
  private ParticleSystem explosion;
  public GameObject body;
  private bool fired = false;
  public string soundy;


  void Start() {
  }

  void Awake()
  {
    explosion = this.GetComponent<ParticleSystem>();
  }

  void OnTriggerEnter(Collider c)
  {
    if (!explosion || fired) return;

    if (c.gameObject.GetComponent<HealthAgent>())
    {
      Debug.Log("Triggered explosion");
      explosion.Play();
      fired = true;

      c.gameObject.GetComponent<HealthAgent>().Damage(damage);

      body.SetActive(false);
      Audio.Play(soundy);
      Destroy(transform.parent.GetComponent<BoxCollider>());
      Destroy(transform.parent.gameObject, explosion.main.duration);
    } else if (!c.isTrigger && transform.parent.gameObject.GetComponent<MissileAI>() != null && transform.parent.gameObject.GetComponent<MissileAI>().GetState() == AIState.Attack) {
      Debug.Log("Triggered explosion");
      explosion.Play();
      fired = true;

      body.SetActive(false);
      Audio.Play(soundy);
      Destroy(transform.parent.GetComponent<BoxCollider>());
      Destroy(transform.parent.gameObject, explosion.main.duration);
    }
  }
}