using UnityEngine;
using System.Collections.Generic;

public class DebugButton : MonoBehaviour
{
    int count = 0;
    Node from;
    Node to;
    List<Node> path;
    public void test(){
        if (count == 0){
            from = AppManager.Instance.m_SelectedNode.GetComponent<Node>();
            from.nodeState.setStart();
            count++;
        }
        else if (count == 1){
            to = AppManager.Instance.m_SelectedNode.GetComponent<Node>();
            to.nodeState.setEnd();
            count++;
        }
        else if (count == 2){
            path = Djikstra.FindShortestPath(GraphManager.Instance.mContainer, from, to);
            foreach (Node n in path){
            Debug.Log(n.nodeName);
            count = 0;
            }
        }

        
    }
}