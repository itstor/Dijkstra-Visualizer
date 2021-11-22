using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropBehaviour : MonoBehaviour
{
    private Vector3 _offset;
    void OnMouseDown() {
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
        gameObject.transform.position = newPosition;
    }
}
