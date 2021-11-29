using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName;
    private TMPro.TextMeshPro _textMesh;
    public Dictionary<Node, float> connectedNodes;

    void Start()
    {
        _textMesh = gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
        _textMesh.text = nodeName;
        connectedNodes = new Dictionary<Node, float>();
    }

    public bool AddNeighbour(Node to, float distance)
    {
        if (!connectedNodes.ContainsKey(to))
        {
            connectedNodes.Add(to, distance);

            Debug.Log(nodeName + " is connected to " + to.nodeName + " with a distance of " + distance);

            return true;
        }

        Debug.Log(nodeName + " is already connected to " + to.nodeName);

        return false;
    }
}
