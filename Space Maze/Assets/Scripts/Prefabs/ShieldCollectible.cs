using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShieldCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Audio.Play("click2sndfx");
            GameState state = GameState.GetGame();

            if (state != null) {
                PlayerState player = state.player;
                player.shields += 1;
            }
            //this.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        
    }
}