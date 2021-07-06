using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scout(int x, int z) {
        
        foreach (Transform t in transform) {
            if (t.localPosition == new Vector3(-75 + (x-1)*20, -15 - z*20) || t.localPosition == new Vector3(-75 + (x+1)*20, -15 - z*20) || t.localPosition == new Vector3(-75 + x*20, -15 - (z-1)*20) || t.localPosition == new Vector3(-75 + x*20, -15 - (z+1)*20)) {
                t.gameObject.SetActive(true);
            }
        }
    }
}
