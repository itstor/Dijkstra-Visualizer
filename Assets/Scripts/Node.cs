using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private string m_nodeNameProperty;
    public Dictionary<Node, EdgeData> m_connectedNodes =  new Dictionary<Node, EdgeData>();
    [HideInInspector] public NodeState m_nodeState;
    [SerializeField] public string m_nodeName { 
        get { 
            return m_nodeNameProperty; 
        } 
        set {
            m_nodeNameProperty = value;
            GetComponent<NodeTextController>().updateNodeName(value); 
        } 
    }


    void Start(){
        m_nodeState = GetComponent<NodeState>();
    }

    // return true if the node is not already connected, false otherwise
    public bool connect(Node to, EdgeData edge_data)
    {
        if (!m_connectedNodes.ContainsKey(to))
        {
            m_connectedNodes.Add(to, edge_data);
            
            return true;
        }

        return false;
    }

    public bool allowConnect(Node to){
        if (!m_connectedNodes.ContainsKey(to))
        {
            return true;
        }
        
        return false;
    }

    /* check if this node already connected to the given node. If so, return true, otherwise false.
    This function is used to change the shape of edge line arrow */
    public bool checkTwoWayConnection(Node from){
        if (m_connectedNodes.ContainsKey(from))
        {
            // Debug.Log("Two Way");
            m_connectedNodes[from].m_isTwoWay = true;

            return true;
        }
        return false;
    }

    public void deleteNode(){
        GraphManager.Instance.deleteNode(this);

        Destroy(this.gameObject);
    }

    public EdgeData getEdgeData(Node node){
        if (m_connectedNodes.ContainsKey(node))
        {
            return m_connectedNodes[node];
        }
        return null;
    }

}
