using UnityEngine;

public class EdgeLineController : MonoBehaviour
{
    static public void updateSingleEdgeLinePosition(LineRenderer line, Vector3 position, int index){
        line.SetPosition(index, new Vector3(position.x, position.y, 0));

        line.GetComponent<EdgeLineChildController>().updateEdgeLinePosition();
    }

    static public void updateMultipleEdgeLinePosition(Node node){
        var edgeList = GraphManager.Instance.getEdgeList(node);

        foreach (var edgeLine in edgeList.Item1)
        {
            updateSingleEdgeLinePosition(edgeLine.gameObject.GetComponent<LineRenderer>(), node.transform.position, 1);
        }

        foreach (var edgeLine in edgeList.Item2)
        {
            updateSingleEdgeLinePosition(edgeLine.gameObject.GetComponent<LineRenderer>(), node.transform.position, 0);
        }
    }
}