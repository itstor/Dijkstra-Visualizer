using UnityEngine;
using System.Collections.Generic;

public class NodeController : MonoBehaviour
{
    private Vector3 mDragOffset;
    private NodeState mNodeState;
    private GameObject mCurrentActiveLine;
    private LineRenderer mCurrentActiveLineRenderer;
    private Node mNnode;

    void Start()
    {
        mNodeState = GetComponent<NodeState>();
        mNnode = GetComponent<Node>();
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
                    mCurrentActiveLine = EdgeLineFactory.Instance.createEdgeLine(gameObject.transform.position);
                    mCurrentActiveLineRenderer = mCurrentActiveLine.GetComponent<LineRenderer>();

                    break;
                default: break;
            }
        }
    }

    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            switch (CursorStateManager.Instance.currentState)
            {
                case states.CursorState.Select:
                    gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mDragOffset;

                    EdgeLineController.updateMultipleEdgeLinePosition(mNnode);
                    
                    break;

                case states.CursorState.Connect:
                    mNodeState.onActive();
                    EdgeLineController.updateSingleEdgeLinePosition(mCurrentActiveLineRenderer, transform.position, 1);
                    
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

            mNodeState.onDeactive();

            if (hit)
            {
                if (hit.collider.gameObject.tag == "Node" && hit.collider.gameObject != gameObject)
                {
                    Vector3 objectPosition = hit.collider.gameObject.transform.position;
                    mCurrentActiveLineRenderer.SetPosition(1, new Vector3(objectPosition.x, objectPosition.y, 0));
                    mCurrentActiveLine.GetComponent<EdgeLineChildController>().updateEdgeLinePosition();
                    
                    var thisNode = GetComponent<Node>();
                    var edgeData = mCurrentActiveLine.GetComponent<EdgeData>();
                    GraphManager.Instance.addEdgeLine(mNnode, thisNode, edgeData);

                    return;
                }
            }
            
            Destroy(mCurrentActiveLine);
        }
    }

    void OnMouseEnter()
    {
        mNodeState.onActive();
    }

    void OnMouseExit()
    {
        mNodeState.onDeactive();
    }
}
