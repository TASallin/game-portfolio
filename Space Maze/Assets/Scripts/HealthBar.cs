using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerState player;
    public GameObject shield;
    public float redFlash;
    public float whiteFlash;
    public static int redLeft;
    public static int whiteLeft;
    public static int maxTimes = 4;
    public float interval = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameState.GetGame().player;
        redLeft = 0;
        whiteLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetGame() == null) {
            return;
        }
        float prevHealth = transform.localScale.x;
        transform.localScale = new Vector3(GameState.GetGame().player.health/100f, 0.15f, 1);
        if (prevHealth > transform.localScale.x) {
            redLeft = 4;
        }

        if (player.shields > 0) {
            //gameObject.GetComponent<Image>().color = new Color(1, 1, 0);
            shield.SetActive(true);
        } else {
            //gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
            shield.SetActive(false);
        }

        if (redLeft > 0) {
            redFlash -= Time.deltaTime;
            if (redFlash <= 0) {
                if (gameObject.GetComponent<Image>().color == new Color(1, 0, 0)) {
                    gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
                    redLeft -= 1;
                } else {
                    gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
                }
                redFlash = interval;
            }
        }

        if (whiteLeft > 0) {
            whiteFlash -= Time.deltaTime;
            if (whiteFlash <= 0) {
                if (gameObject.GetComponent<Image>().color == new Color(1, 1, 1)) {
                    gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
                    whiteLeft -= 1;
                } else {
                    gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                whiteFlash = interval;
            }
        }
    }

    
}
