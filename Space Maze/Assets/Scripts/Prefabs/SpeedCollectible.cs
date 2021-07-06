using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedCollectible : MonoBehaviour
{
    private IEnumerator speedUpCoroutine;
    public float newSpeed = 16f;
    public float buffTime = 7f;
    private PlayerState player;
    private Renderer rend;
    void Start()
    {
        player = GameState.GetGame().player;
        rend = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            speedUpCoroutine = SetSpeedUp(buffTime);
            StartCoroutine(speedUpCoroutine);
            rend.enabled = false;
        }
    }

    private IEnumerator SetSpeedUp(float buffTime)
    {
        float oldSpeed = player.speed;
        player.speed = newSpeed;

        Debug.Log("Player speed is now: " + player.speed.ToString());

        Debug.Log("Should wait for: " + buffTime.ToString() + " seconds");

        yield return new WaitForSeconds(buffTime);

        Debug.Log("Switching back to old speed: " + oldSpeed.ToString());
        player.speed = oldSpeed;

        this.gameObject.SetActive(false);
    }
}