using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed, dragSpeed;
    public float smoothZoomValue;
    public float minZoom, maxZoom;
    private float initialZoom;

    private float _targetZoomValue;
    private bool _isClicked = false;
    private Vector3 _mouseClickedPosition;
    private Camera _activeCamera;

    void Start()
    {
        _activeCamera = Camera.main;
        _targetZoomValue = _activeCamera.orthographicSize;
        initialZoom = _activeCamera.orthographicSize;
    }

    void Update()
    {
        //if mouse on held, drag camera based on the difference between the mouse position and the mouse position on click.
        if (_isClicked)
        {
            Vector3 currMousePos = Input.mousePosition;
            Vector3 dragPosDiff = currMousePos - _mouseClickedPosition;
            _activeCamera.transform.position -= dragPosDiff * dragSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            _targetZoomValue -= scroll * zoomSpeed;
            _targetZoomValue = Mathf.Clamp(_targetZoomValue, minZoom, maxZoom);
            // Debug.Log(_targetZoomValue);
        }

        //get mouse position before zoom
        Vector3 mousePos1 = getMouseWorldPoint();

        _activeCamera.orthographicSize = Mathf.Lerp(_activeCamera.orthographicSize, _targetZoomValue, smoothZoomValue);

        if (Mathf.RoundToInt(_activeCamera.orthographicSize) != Mathf.RoundToInt(_targetZoomValue))
        {
            GUIManager.Instance.showToast($"Zoom {(Mathf.Round((maxZoom - _activeCamera.orthographicSize)/(maxZoom - initialZoom) * 100))}%", 2f, true);
            // Debug.Log("here");
        }
        //get mouse position after zoom
        Vector3 mousePos2 = getMouseWorldPoint();
        
        Vector3 mousePositionDiff = mousePos1 - mousePos2;

        Vector3 targetPos = _activeCamera.transform.position + mousePositionDiff;
        _activeCamera.transform.position = targetPos;

        //if middle mouse button is clicked, set origin of drag
        if (Input.GetMouseButton(2))
        {
            _mouseClickedPosition = Input.mousePosition;
            _isClicked = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            _isClicked = false;
        }
    }

    private Vector3 getMouseWorldPoint()
    {
        return _activeCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
