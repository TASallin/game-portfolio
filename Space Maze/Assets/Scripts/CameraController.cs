using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float targetZ;
    public float targetX;

    // Start is called before the first frame update
    void Start()
    {
        targetZ = gameObject.transform.position.z;
        targetX = gameObject.transform.position.x;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = gameObject.transform.position;
        if (currPos.x != targetX || currPos.z != targetZ) {
            GameState.GetGame().timeScale = 0;
            float newX, newZ;
            if (targetX > currPos.x) {
                newX = System.Math.Min(currPos.x + Time.unscaledDeltaTime*40f, targetX);
            } else {
                newX = System.Math.Max(currPos.x - Time.unscaledDeltaTime*40f, targetX);
            }
            if (targetZ > currPos.z) {
                newZ = System.Math.Min(currPos.z + Time.unscaledDeltaTime*40f, targetZ);
            } else {
                newZ = System.Math.Max(currPos.z - Time.unscaledDeltaTime*40f, targetZ);
            }
            gameObject.transform.position = new Vector3(newX, currPos.y, newZ);
            if (gameObject.transform.position.x == targetX && gameObject.transform.position.z == targetZ) {
                GameState.GetGame().timeScale = 1;
                Debug.Log("Now");
            }
        }
       
    }

    public void SetTarget(float x, float z) {
        targetX = x;
        targetZ = z - 3;
    }
}
