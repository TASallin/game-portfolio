using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    public PathNetwork network;
    public PathNode location;
    Vector3 target;
    public float speed = 1;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = Vector3.Normalize(target - transform.position);
        float distance = System.Math.Min((speed + 2*(GameState.GetGame().level-1)) * Time.deltaTime, (target-transform.position).magnitude);
        //Debug.Log(distance);
        transform.Translate(distance * direction);
        if (!started || target == null || (target - transform.position).magnitude <= 0.05) {
            ChooseNode();
        }
    }


    void ChooseNode() {
        if (location == null) {
            return;
        }
        List<PathNode> neighbors = location.GetNeighbors();
        if (neighbors.Count == 0) {
            return;
        } else if (neighbors.Count == 1) {
            location = neighbors[0];
        } else {
            location = neighbors[new System.Random().Next(neighbors.Count)];
        }
        target = location.gameObject.transform.position;
        started = true;
        target = new Vector3(target.x, transform.position.y, target.z);
    }
}
