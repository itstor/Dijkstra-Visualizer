using UnityEngine;
using System.Collections.Generic;

public class NodeController : MonoBehaviour
{
    private Vector3 mDragOffset;
    private NodeState mNodeState;
    private GameObject mCurrentActiveLine;
    private LineRenderer mCurrentActiveLineRenderer;
    private Node mNode;

    void Start()
    {
        mNodeState = GetComponent<NodeState>();
        mNode = GetComponent<Node>();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            switch (CursorStateManager.Instance.currentState)
            {
                case states.CursorState.Select:
                    mDragOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

                    break;

                case states.CursorState.Connect:
                    mCurrentActiveLine = ObjectFactory.Instance.createEdgeLine(gameObject.transform.position);
                    mCurrentActiveLineRenderer = mCurrentActiveLine.GetComponent<LineRenderer>();

                    break;
                default: break;
            }
        }
        // Debug.Log("Mouse Down" + Input.GetMouseButton(1));
    }

    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            switch (CursorStateManager.Instance.currentState)
            {
                case states.CursorState.Select:
                    gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mDragOffset;

                    EdgeLineController.updateMultipleEdgeLinePosition(GraphManager.Instance.getEdgeList(mNode), transform.position);

                    break;

                case states.CursorState.Connect:
                    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mNodeState.onHover();
                    EdgeLineController.updateSingleEdgeLinePosition(mCurrentActiveLineRenderer, mousePosition, 1);

                    if (mCurrentActiveLine.activeInHierarchy == false)
                    {
                        mCurrentActiveLine.SetActive(true);
                    }

                    break;
                default: break;
            }
        }
    }

    void OnMouseUp()
    {
        if (CursorStateManager.Instance.currentState == states.CursorState.Connect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            mNodeState.onExitHover();
            
            if (hit.collider == null){
                Destroy(mCurrentActiveLine);
                return;
            }

            if (hit.collider.gameObject.tag == "Node" && hit.collider.gameObject != gameObject)
            {
                Vector3 objectPosition = hit.collider.gameObject.transform.position;
                var otherNode = hit.collider.gameObject.GetComponent<Node>();
                var edgeData = mCurrentActiveLine.GetComponent<EdgeData>();

                mCurrentActiveLineRenderer.SetPosition(1, new Vector3(objectPosition.x, objectPosition.y, 0));
                mCurrentActiveLine.GetComponent<EdgeLineChildController>().updateEdgeLinePosition();

                GUIManager.Instance.showDialog(1, (string distance, bool isInput, Dictionary<string, dynamic> Object) =>
                {
                    if (isInput)
                    {
                        Object["edgeData"].distance = int.Parse(distance);
                        if (Object["mNode"].connect(Object["otherNode"], Object["edgeData"]))
                        {

                                // TODO : dirty method. gonna find another method
                                if (Object["otherNode"].checkTwoWayConnection(Object["mNode"]))
                            {
                                Object["mNode"].getEdgeData(Object["otherNode"]).GetComponent<EdgeData>().isTwoWay = true;
                            }
                            GraphManager.Instance.addEdgeLine(from: Object["mNode"], to: Object["otherNode"], edge_data: Object["edgeData"]);
                        }
                    }
                    else
                    {
                        Destroy(Object["edgeData"].gameObject);
                    }
                }, new Dictionary<string, dynamic> { 
                    ["mNode"] = mNode,
                    ["otherNode"] = otherNode,
                    ["edgeData"] = edgeData
                });
            }
            else
            {
                Destroy(mCurrentActiveLine);
            }
        }
    }

    void OnMouseEnter()
    {
        // Debug.Log("Mouse enter");
        mNodeState.onHover();
    }

    void OnMouseExit()
    {
        mNodeState.onExitHover();
    }
}
