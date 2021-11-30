using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName {get; set;}
    public Dictionary<Node, EdgeData> connectedNodes;

    void Start()
    {
        connectedNodes = new Dictionary<Node, EdgeData>();
    }

    // return true if the node is not already connected, false otherwise
    public bool connect(Node to, EdgeData edge_data)
    {
        if (!connectedNodes.ContainsKey(to))
        {
            connectedNodes.Add(to, edge_data);
            return true;
        }

        return false;
    }

    /* check if this node already connected to the given node. If so, return true, otherwise false.
    This function is used to change the shape of edge line arrow */
    public void checkTwoWayConnection(Node from){
        if (connectedNodes.ContainsKey(from))
        {
            connectedNodes[from].isTwoWay = true;
        }
    }

    public void deleteNode(){
        Destroy(this.gameObject);
        
        // TODO delete all the edge lines connected to this node
    }
}
