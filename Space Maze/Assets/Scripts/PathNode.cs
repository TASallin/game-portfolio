using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public List<PathNode> neighbors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableRender()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void AddNeighbor(PathNode p) {
        if (neighbors == null) {
            neighbors = new List<PathNode>();
        }
        neighbors.Add(p);
    }

    public List<PathNode> GetNeighbors() {
        if (neighbors == null) {
            neighbors = new List<PathNode>();
        }
        return neighbors;
    }
}
