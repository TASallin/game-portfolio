using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthCollectible : MonoBehaviour
{
    public float healthBuff = 30f;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player") && GameState.GetGame().player.health < PlayerState.MAX_HEALTH)
        {
            Audio.Play("click2sndfx");
            PlayerState player = GameState.GetGame().player;
            if (player == null) return;

            player.health += healthBuff;
            if (player.health > PlayerState.MAX_HEALTH) player.health = PlayerState.MAX_HEALTH;
            Debug.Log("Restored life, player health is now " + player.health.ToString());
            Destroy(this.gameObject);
        } else if (collider.gameObject.tag.Equals("Player")) {
            HealthBar.whiteLeft = HealthBar.maxTimes;
            Audio.Play("click2sndfx");
        }
    }
}