using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph: MonoBehaviour
{
    [SerializeField] public List<Node> Nodes;

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public void AddNode(Node node)
    {
        Nodes.Add(node);
    }

    public void RemoveNode(Node node){
        Nodes.Remove(node);
    }

    public void ConnectNodes(Node from, Node to, float distance)
    {
        from.AddNeighbour(to, distance);
    }

    public void DisconnectNodes(Node from, Node to)
    {
        //from.RemoveNeighbour(to);
    }
}