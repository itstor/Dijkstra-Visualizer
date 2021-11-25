using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName;
    private TMPro.TextMeshPro _textMesh;
    public Dictionary<Node, float> connectedNodes;
    // Start is called before the first frame update
    void Start()
    {
        _textMesh = gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
        _textMesh.text = nodeName;
        connectedNodes = new Dictionary<Node, float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(ref Node node, float distance)
    {
        connectedNodes.Add(node, distance);
    }
}
