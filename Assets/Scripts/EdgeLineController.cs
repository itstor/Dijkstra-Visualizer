using System.Collections.Generic;
using UnityEngine;

public class EdgeLineController : MonoBehaviour
{
    static public void updateSingleEdgeLinePosition(LineRenderer line, Vector3 position, int index){
        line.SetPosition(index, new Vector3(position.x, position.y, 0));

        line.GetComponent<EdgeLineChildController>().updateEdgeLinePosition();
    }

    static public void updateMultipleEdgeLinePosition((List<EdgeData>, List<EdgeData>) listOfEdge, Vector3 position){
        Debug.Log("Incoming: " + listOfEdge.Item1.Count + " Outgoing: " + listOfEdge.Item2.Count);
        
        foreach (var edgeLine in listOfEdge.Item1)
        {
            updateSingleEdgeLinePosition(edgeLine.gameObject.GetComponent<LineRenderer>(), position, index: 1);
        }

        foreach (var edgeLine in listOfEdge.Item2)
        {
            updateSingleEdgeLinePosition(edgeLine.gameObject.GetComponent<LineRenderer>(), position, index: 0);
        }
    }
}