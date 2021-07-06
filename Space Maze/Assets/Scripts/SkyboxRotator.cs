using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{   
    public float rotationMod = 1;
    public List<Material> skyboxes;
    private float rotation;
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        try {
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
        rotation += Time.deltaTime * rotationMod;
        if (rotation > 360) {
            rotation -= 360;
        }
        


        if (GameState.GetGame().level != level) {
            level = GameState.GetGame().level;
            RenderSettings.skybox = skyboxes[level - 1];
        }
        } catch {
            
        }
    }
}
