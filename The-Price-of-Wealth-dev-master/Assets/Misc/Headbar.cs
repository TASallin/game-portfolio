using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Headbar : MonoBehaviour
{
    public static Headbar instance;

    [HideInInspector]
    public TextMeshProUGUI title;

    void Awake() {
        instance = this;
        title = transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        string prefix = SceneManager.GetActiveScene().name;
        if (prefix == "Overworld" || prefix == "Dungeon") {
            title.text = prefix;
        } else {
            title.text = Areas.GetLocationFormatted(null) + " - " + prefix;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
