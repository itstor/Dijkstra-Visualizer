using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperty : MonoBehaviour
{
    public string nodeName;
    private TMPro.TextMeshPro _textMesh;
    public List<NodeProperty> connectedNodes;
    // Start is called before the first frame update
    void Start()
    {
        _textMesh = gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
        _textMesh.text = nodeName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
