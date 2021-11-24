using UnityEngine;

public class NodeMouseControl : MonoBehaviour
{
    private Vector3 _dragOffset;
    private SpriteRenderer _nodeSprite;
    private bool _isHover;
    [SerializeField] private float _fadeSpeed;

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
    }

    void OnMouseDown() {
        if (Input.GetMouseButton(0) && CursorStateManager.Instance.currentState == CursorStateManager.CursorState.Select)
            _dragOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0) && CursorStateManager.Instance.currentState == CursorStateManager.CursorState.Select){
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _dragOffset;
            gameObject.transform.position = newPosition;
        }
    }

    void OnMouseEnter() {
        _isHover = true;
    }

    void OnMouseExit() {
        _isHover = false;
    }
}
