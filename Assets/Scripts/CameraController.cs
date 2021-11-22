using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _activeCamera;
    public float zoomSpeed, dragSpeed;
    public float smoothZoomValue;
    public float minZoom, maxZoom;
    private float _targetZoomValue;
    private Vector3 _mouseClickedPosition;
    private bool _isClicked = false;

    private void Start()
    {
        _activeCamera = Camera.main;
        _targetZoomValue = _activeCamera.orthographicSize;
    }

    void Update()
    {
        //if mouse on held, drag camera based on the difference between the mouse position and the mouse position on click.
        if (_isClicked)
        {
            Vector3 currMousePos = Input.mousePosition;
            Vector3 dragPosDiff = currMousePos - _mouseClickedPosition;
            _activeCamera.transform.position += dragPosDiff * dragSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            _targetZoomValue -= scroll * zoomSpeed;
            _targetZoomValue = Mathf.Clamp(_targetZoomValue, minZoom, maxZoom);
        }
        //get mouse position before zoom
        Vector3 mousePos1 = _activeCamera.ScreenToWorldPoint(Input.mousePosition);

        _activeCamera.orthographicSize = Mathf.Lerp(_activeCamera.orthographicSize, _targetZoomValue, smoothZoomValue);

        //get mouse position after zoom
        Vector3 mousePos2 = _activeCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePositionDiff = mousePos1 - mousePos2;

        Vector3 targetPos = _activeCamera.transform.position + mousePositionDiff;
        _activeCamera.transform.position = targetPos;

        //if mouse is clicked, set origin of drag
        if (Input.GetMouseButton(0))
        {
            _mouseClickedPosition = Input.mousePosition;
            _isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isClicked = false;
        }
    }
}
