using UnityEngine;
using System.Collections.Generic;

public class NodeMouseControl : MonoBehaviour
{
    private Vector3 _dragOffset;
    private List<GameObject> _edgeLinePrefabs;
    public GameObject _edgeLinePrefab;
    private GameObject _currentActiveLine;
    private List<GameObject> _edgeLineNeighbourPrefabs;
    private INodeStateController _modeControl;


void OnDestroy() {
 for (int i = 0; i < _edgeLinePrefabs.Count; i++) {
     Destroy(_edgeLinePrefabs[i]);
 }   
}
    void Start()
    {
        _modeControl = GetComponent<NodeStateController>();
        _edgeLinePrefabs = new List<GameObject>();
        _edgeLineNeighbourPrefabs = new List<GameObject>();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            switch (CursorStateManager.Instance.currentState)
            {
                case CursorStateManager.CursorState.Select:
                    _dragOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    break;
                case CursorStateManager.CursorState.Connect:
                    GameObject newLine = Instantiate(_edgeLinePrefab, transform.position, Quaternion.identity);
                    InitLinePosition(newLine);
                    _currentActiveLine = newLine;
                    _edgeLinePrefabs.Add(newLine);
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
                case CursorStateManager.CursorState.Select:
                    Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _dragOffset;
                    gameObject.transform.position = newPosition;
                    UpdateAllLinePosition();
                    break;
                case CursorStateManager.CursorState.Connect:
                    _modeControl.OnSelected();
                    UpdateSingleLinePosition(_currentActiveLine);
                    break;
                default: break;
            }
        }
    }

    void OnMouseUp()
    {
        if (CursorStateManager.Instance.currentState == CursorStateManager.CursorState.Connect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            _modeControl.OnDeselected();

            if (hit)
            {
                if (hit.collider.gameObject.tag == "Node" && hit.collider.gameObject != gameObject)
                {
                    Vector3 objectPosition = hit.collider.gameObject.transform.position;
                    _currentActiveLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(objectPosition.x, objectPosition.y, 0));
                    _currentActiveLine.GetComponent<EdgeLineScript>().updateEdgeLinePos();
                    hit.collider.gameObject.GetComponent<NodeMouseControl>()._edgeLineNeighbourPrefabs.Add(_currentActiveLine);

                    // hit.collider.gameObject.GetComponent<Node>().Add(gameObject.GetComponent<Node>(), 100);

                    return;
                }
            }
            
            _edgeLinePrefabs.Remove(_currentActiveLine);
            Destroy(_currentActiveLine);
        }
    }

    void OnMouseEnter()
    {
        _modeControl.OnSelected();
    }

    void OnMouseExit()
    {
        _modeControl.OnDeselected();
    }

    void InitLinePosition(GameObject line)
    {
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        
        lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
        lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
        line.GetComponent<EdgeLineScript>().updateEdgeLinePos();
    }

    void UpdateSingleLinePosition(GameObject line, int index = 1, bool drag_node = false)
    {
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!drag_node)
        {
            lineRenderer.SetPosition(index, new Vector3(mousePosition.x, mousePosition.y, 0));
        }
        else
        {
            lineRenderer.SetPosition(index, new Vector3(transform.position.x, transform.position.y, 0));
        }
        line.GetComponent<EdgeLineScript>().updateEdgeLinePos();
    }

    void UpdateAllLinePosition(){
        foreach (GameObject line in _edgeLinePrefabs)
        {
            UpdateSingleLinePosition(line, 0, true);
        }
        foreach (GameObject line in _edgeLineNeighbourPrefabs)
        {
            UpdateSingleLinePosition(line, 1, true);
        }
    }

    void AddEdgeLineNeighbour(GameObject line)
    {
        _edgeLineNeighbourPrefabs.Add(line);
    }
}
