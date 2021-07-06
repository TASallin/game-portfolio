using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter: MonoBehaviour
{
  public GameObject plot;
  public GameObject menu;
  public void StartGame()
  {
    GameState.StartGame();
    Timer.startTime = Time.time;
    SceneManager.LoadScene("Master Scene");
  }

  public void Plot() {
    plot.SetActive(true);
    menu.SetActive(false);
  }
}