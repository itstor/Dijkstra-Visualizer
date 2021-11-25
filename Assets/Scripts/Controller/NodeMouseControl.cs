using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMouseControl : MonoBehaviour
{
    private Vector3 _dragOffset;
    private SpriteRenderer _nodeSprite;
    private bool _isHover;
    [SerializeField] private float _fadeSpeed;
    private List<GameObject> _edgeLinePrefabs;
    public GameObject _edgeLinePrefab;
    private GameObject _currentActiveLine;
    private Color _originalColor;
    public GameObject _glowEffect;


    void Start()
    {
        _nodeSprite = GetComponent<SpriteRenderer>();
        _edgeLinePrefabs = new List<GameObject>();
        _originalColor = _nodeSprite.color;
    }

    void Update()
    {
        if (_isHover)
        {
            float newScale = Mathf.SmoothStep(_glowEffect.transform.localScale.x, 1f, _fadeSpeed * Time.deltaTime);
            _glowEffect.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        else
        {
            float newScale = Mathf.SmoothStep(_glowEffect.transform.localScale.x, 0.5f, _fadeSpeed * Time.deltaTime);
            _glowEffect.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
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
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Node" && hit.collider.gameObject != gameObject)
                {
                    Vector3 objectPosition = hit.collider.gameObject.transform.position;
                    _currentActiveLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(objectPosition.x, objectPosition.y, 0));
                    _currentActiveLine.GetComponentInChildren<EdgeArrowScript>().UpdateArrowPosition();
                    return;
                }
            }
            Debug.Log("Miss");
            _edgeLinePrefabs.Remove(_currentActiveLine);
            Destroy(_currentActiveLine);
        }
    }

    void OnMouseEnter()
    {
        _isHover = true;
    }

    void OnMouseExit()
    {
        _isHover = false;
    }

    void InitLinePosition(GameObject line)
    {
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        
        lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
        lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
        line.GetComponentInChildren<EdgeArrowScript>().UpdateArrowPosition();
    }

    void UpdateSingleLinePosition(GameObject line, int index = 1)
    {
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(index, new Vector3(mousePosition.x, mousePosition.y, 0));
        line.GetComponentInChildren<EdgeArrowScript>().UpdateArrowPosition();
    }

    void UpdateAllLinePosition(){
        foreach (GameObject line in _edgeLinePrefabs)
        {
            UpdateSingleLinePosition(line, 0);
        }
    }
}
