using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject followee;
    public float speed = 1.5f;
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.z = Mathf.Lerp(this.transform.position.z, followee.transform.position.z, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, followee.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
