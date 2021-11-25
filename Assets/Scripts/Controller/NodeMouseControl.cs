using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMouseControl : MonoBehaviour
{
    private Vector3 _dragOffset;
    private SpriteRenderer _nodeSprite;
    private bool _isHover;
    [SerializeField] private float _fadeSpeed;
    private List<LineRenderer> _edgeLines;
    private LineRenderer _lineOri;

    void Update() {
        if (_isHover){
            _nodeSprite.color = Color.Lerp(_nodeSprite.color, Color.red, Time.deltaTime * _fadeSpeed);
        }
        else {
            _nodeSprite.color = Color.Lerp(_nodeSprite.color, Color.white, Time.deltaTime * _fadeSpeed);
        }
    }

    void Start()
    {
        _nodeSprite = GetComponent<SpriteRenderer>();
        _edgeLines = new List<LineRenderer>();
        // _lineOri = gameObject.Get
        InitLinePosition();
    }

    void OnMouseDown() {
        if (Input.GetMouseButton(0)){
            switch (CursorStateManager.Instance.currentState)
            {
                case CursorStateManager.CursorState.Select:
                    _dragOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                break;
                case CursorStateManager.CursorState.Connect:
                    // Instantiate
                break;
                default: break;
            }
        }
    }

    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0)){
            switch (CursorStateManager.Instance.currentState)
            {
                case CursorStateManager.CursorState.Select:
                    Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _dragOffset;
                    gameObject.transform.position = newPosition;
                    UpdateLinePosition();
                break;
                case CursorStateManager.CursorState.Connect:
                    // _edgeLine.SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                break;
                default: break;
            }
        }
    }

    void OnMouseUp()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Node" && hit.collider.gameObject != gameObject){
                    Debug.Log("Hit");
                    Debug.Log(hit.collider.gameObject.GetComponentInChildren<TMPro.TextMeshPro>().text);
                }
            }
    }

    void OnMouseEnter() {
        _isHover = true;
    }

    void OnMouseExit() {
        _isHover = false;
    }
    
    void InitLinePosition(){
        // _edgeLine.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
        // _edgeLine.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
    }

    void UpdateLinePosition(){
        // _edgeLine.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
    }
}
