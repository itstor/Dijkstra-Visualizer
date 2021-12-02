using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public string nodeName{set;get;}
    public Dictionary<Node, EdgeData> connectedNodes =  new Dictionary<Node, EdgeData>();

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
    public bool checkTwoWayConnection(Node from){
        if (connectedNodes.ContainsKey(from))
        {
            Debug.Log("Two Way");
            connectedNodes[from].isTwoWay = true;

            return true;
        }
        return false;
    }

    public void deleteNode(){
        GraphManager.Instance.deleteNode(this);

        Destroy(this.gameObject);
    }

    public EdgeData getEdgeData(Node node){
        if (connectedNodes.ContainsKey(node))
        {
            return connectedNodes[node];
        }
        return null;
    }

}
