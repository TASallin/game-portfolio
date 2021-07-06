using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
  public Vector3 portalDestination = new Vector3(0, 0, 0);


  void OnTriggerEnter(Collider collider) {
    Debug.Log("Collide with portal");

    if (collider.gameObject.tag.Equals("Player")) {
      Debug.Log("player initial position is " + collider.gameObject.transform.position);

      Debug.Log("Teleporting player");
      Debug.Log("Destination is " + portalDestination.ToString());

      collider.gameObject.transform.position = portalDestination;
    }
  }
}