using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNetwork : MonoBehaviour
{
    List<PathNode> vertices;
    List<(PathNode, PathNode)> edges;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNeighbors() {
        foreach ((PathNode, PathNode) edge in edges) {
            //var (first, second) = edge;
            //first.AddNeighbor(second);
            //second.AddNeighbor(first);
            edge.Item1.AddNeighbor(edge.Item2);
            edge.Item2.AddNeighbor(edge.Item1);
        }
    }

    public void SetEdges(List<(PathNode, PathNode)> e) {
        edges = e;
    }

    public void AddEdge(PathNode one, PathNode two) {
        if (edges == null) {
            edges = new List<(PathNode, PathNode)>();
        }
        edges.Add((one, two));
    }

    public void CalculateVertices() {
        PathNode[] nodes = GetComponentsInChildren<PathNode>();
        vertices = new List<PathNode>();
        foreach (PathNode p in nodes) {
            vertices.Add(p);
        }
    }
}
