using System.Collections;
using UnityEngine;

public class MineExplosionManager : MonoBehaviour
{
  public int damage = 50;
  private ParticleSystem explosion;
  private IEnumerator explosionCleanup;
  void Awake()
  {
    explosion = this.GetComponent<ParticleSystem>();
  }
  void OnTriggerEnter(Collider c)
  {
    if (c.gameObject.GetComponent<HealthAgent>() != null)
    {
      if (!explosion) return;

      Debug.Log("Triggered explosion");
      explosion.Play();

      Destroy(gameObject, explosion.main.duration);

      c.gameObject.GetComponent<HealthAgent>().Damage(damage);

      explosionCleanup = DestroyExplosion(explosion.main.duration);
      StartCoroutine(explosionCleanup);
    }
  }

  private IEnumerator DestroyExplosion(float explosionDuration)
  {
    yield return new WaitForSeconds(explosionDuration);
    Destroy(transform.parent.parent.gameObject);
    Destroy(gameObject);
  }
}