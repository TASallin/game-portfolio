using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    //public CameraController globalCamera;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnHere() {
        foreach (Agent a in gameObject.transform.parent.gameObject.GetComponentsInChildren<Agent>(true)) {
           a.SpawnHere();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
           Camera.main.GetComponent<CameraController>().SetTarget(gameObject.transform.position.x, gameObject.transform.position.z - 25);
           gameObject.transform.parent.gameObject.GetComponent<Room>().Enter();
           foreach (Agent a in gameObject.transform.parent.gameObject.GetComponentsInChildren<Agent>(true)) {
               a.InRoom();
           }
        }
    }

    void OnTriggerExit(Collider other) {
         if (other.gameObject.tag == "Player") {
            gameObject.transform.parent.gameObject.GetComponent<Room>().Exit();
           foreach (Agent a in gameObject.transform.parent.gameObject.GetComponentsInChildren<Agent>(true)) {
               a.LeaveRoom();
           }
        }
    }
}
