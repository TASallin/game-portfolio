using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
  private CanvasGroup canvasGroup;
  public GameObject gameOver;
  private PlayerState player;
  private bool showMenu = false;
  private bool dead = false;
  void Awake()
  {
    canvasGroup = GetComponent<CanvasGroup>();
    player = GameState.GetGame().player;

    if (canvasGroup == null || !(canvasGroup is CanvasGroup))
    {
      Debug.Log("CanvasGroup component failed to attach");
    }
  }

  void Update()
  {
    if (showMenu || dead) {
      {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
        if (dead) {
          gameOver.SetActive(true);
        } else {
          gameOver.SetActive(false);
        }
      }
    } else {
      canvasGroup.interactable = false;
      canvasGroup.blocksRaycasts = false;
      canvasGroup.alpha = 0f;
      Time.timeScale = GameState.GetGame().timeScale;
    }
    if (Input.GetKeyUp(KeyCode.Escape)) showMenu = !showMenu;
    if (player.health <= 0) dead = true;
  }
}