using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    //Maybe these become private later idk
    public static float MAX_HEALTH = 200;
    public float health {
        get;
        set;
    } = MAX_HEALTH;
    public float topSpeed = 2f;
    public float acceleration = 1f;
    public float attack = 1f;
    public float size = 1f;
    public bool isTakingDamage = false;
    public int shields = 0;
    public float speed = 8f;
}
