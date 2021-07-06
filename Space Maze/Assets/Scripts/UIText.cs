using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    public Text levelText;
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level 1";
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetGame().level != level) {
            level = GameState.GetGame().level;
            levelText.text = "Level " + (char)(level+48);
        }
    }
}
