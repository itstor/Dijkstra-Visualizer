using UnityEngine;

public class NodeMouseControl : MonoBehaviour
{
    private Vector3 _dragOffset;
    public SpriteRenderer nodeSprite;

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
        nodeSprite.color = Color.red;
    }

    void OnMouseExit() {
        nodeSprite.color = Color.white;
    }
}
