using UnityEngine;

public class NodeTextWatcher : MonoBehaviour {

    private TMPro.TextMeshPro mTextMesh;
    private Node mNode;
    void Start() {
        mTextMesh = GetComponentInChildren<TMPro.TextMeshPro>();    
        mTextMesh.text = GetComponent<Node>().nodeName;
    }

    void Update() {
        if (mTextMesh.text != mNode.nodeName) {
            mTextMesh.text = mNode.nodeName;
        }
    }
}

