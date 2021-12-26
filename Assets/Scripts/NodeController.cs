using UnityEngine;
using System.Collections.Generic;

public class NodeController : MonoBehaviour
{
    private Vector3 m_dragOffset;
    private NodeState m_nodeState;
    private GameObject m_currentActiveLine;
    private LineRenderer m_currentActiveLineRenderer;
    private Node m_node;

    void Start()
    {
        m_nodeState = GetComponent<NodeState>();
        m_node = GetComponent<Node>();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            switch (CursorStateManager.Instance.m_currentState)
            {
                case states.CursorState.Select:
                    m_dragOffset = gameObject.transform.position - Utils.getMouseWorldPosition();;

                    break;

                case states.CursorState.Connect:
                    m_currentActiveLine = ObjectFactory.Instance.createEdgeLine(gameObject.transform.position);
                    m_currentActiveLineRenderer = m_currentActiveLine.GetComponent<LineRenderer>();

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
            switch (CursorStateManager.Instance.m_currentState)
            {
                case states.CursorState.Select:
                    gameObject.transform.position = Utils.getMouseWorldPosition() + m_dragOffset;

                    EdgeLineController.updateMultipleEdgeLinePosition(GraphManager.Instance.getEdgeList(m_node), transform.position);

                    break;

                case states.CursorState.Connect:
                    var mousePosition = Utils.getMouseWorldPosition();
                    m_nodeState.setHover();
                    EdgeLineController.updateSingleEdgeLinePosition(m_currentActiveLineRenderer, mousePosition, 1);

                    if (m_currentActiveLine.activeInHierarchy == false)
                    {
                        m_currentActiveLine.SetActive(true);
                    }

                    break;
                default: break;
            }
        }
    }

    void OnMouseUp()
    {
        if (CursorStateManager.Instance.m_currentState == states.CursorState.Connect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            m_nodeState.setExitHover();
            
            if (hit.collider == null){
                Destroy(m_currentActiveLine);
                return;
            }

            if (hit.collider.gameObject.tag == "Node" && hit.collider.gameObject != gameObject)
            {
                Vector3 objectPosition = hit.collider.gameObject.transform.position;
                var otherNode = hit.collider.gameObject.GetComponent<Node>();
                var edgeData = m_currentActiveLine.GetComponent<EdgeData>();

                m_currentActiveLineRenderer.SetPosition(1, new Vector3(objectPosition.x, objectPosition.y, 0));
                m_currentActiveLine.GetComponent<EdgeLineChildController>().updateEdgeLinePosition();

                // TODO : dirty method. gonna find another method
                if (otherNode.checkTwoWayConnection(m_node))
                {
                    var otherEdgeData = otherNode.getEdgeData(m_node).GetComponent<EdgeData>();
                    otherEdgeData.m_isTwoWay = true;
                    m_node.connect(otherNode, otherEdgeData);
                    GraphManager.Instance.addEdgeLine(from: otherNode, to: m_node, edge_data: otherEdgeData);
                }
                else if (m_node.allowConnect(otherNode)) {
                    edgeData.m_distance = Utils.calculateDistance2Point(transform.position, objectPosition);
                    edgeData.m_fromPosition = transform.position;
                    edgeData.m_toPosition = objectPosition;
                    GraphManager.Instance.addEdgeLine(from: m_node, to: otherNode, edge_data: edgeData);
                    m_node.connect(otherNode, edgeData);
                    GUIManager.Instance.showToast($"Connected {m_node.m_nodeName} to {otherNode.m_nodeName}", 2f);

                    return;
                }
            }
            Destroy(m_currentActiveLine);
        }
    }

    void OnMouseEnter()
    {
        // Debug.Log("Mouse enter");
        m_nodeState.setHover();
    }

    void OnMouseExit()
    {
        m_nodeState.setExitHover();
    }
}
