using UnityEngine.SceneManagement;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
  public void QuitGame()
  {
    Application.Quit();
  }

  public void ToMain() {
    SceneManager.LoadScene("GameMenuScene");
  }
}