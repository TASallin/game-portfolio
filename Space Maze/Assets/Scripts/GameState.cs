using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{

    private static GameState game; //singleton here

    public float damageMult; //Modifies the damage of all enemies, for difficulty creep
    public float healthMult; //Modifies the hp of all enemies
    public PlayerState player;
    public int level;
    public System.Random rng;
    public float timeScale;
    public int score;

    public GameState() {
        damageMult = 1.0f;
        healthMult = 1.0f;
        level = 1;
        timeScale = 1f;
        player = new PlayerState();
        score = 0;
    }

    //Gets the cannon GameState, call often
    public static GameState GetGame() {
        if (game == null) {
            game = new GameState();
        }
        return game;
    }

    public static void StartGame() {
        game = new GameState();
    }
}
