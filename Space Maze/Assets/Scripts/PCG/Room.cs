using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int x;
    public int z;
    public GameObject mapSquare;
    public Music jukebox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter() {
        mapSquare.SetActive(true);
        mapSquare.GetComponent<Image>().color = new Color(0, 1, 0);
        mapSquare.transform.parent.gameObject.GetComponent<Minimap>().Scout(x, z);
        if (jukebox != null) {
            jukebox.Boss();
        }
    }

    public void Exit() {
        mapSquare.GetComponent<Image>().color = new Color(0, 0, 0);
    }
}
