using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float m_zoomSpeed, m_dragSpeed;
    public float m_smoothZoomValue;
    public float m_minZoom, m_maxZoom;
    private float m_initialZoom;

    private float m_targetZoomValue;
    private bool m_isClicked = false;
    private Vector3 m_mouseClickedPosition;
    private Camera m_activeCamera;

    void Start()
    {
        m_activeCamera = Camera.main;
        m_targetZoomValue = m_activeCamera.orthographicSize;
        m_initialZoom = m_activeCamera.orthographicSize;
    }

    void Update()
    {
        //if mouse on held, drag camera based on the difference between the mouse position and the mouse position on click.
        if (m_isClicked)
        {
            Vector3 currMousePos = Input.mousePosition;
            Vector3 dragPosDiff = currMousePos - m_mouseClickedPosition;
            m_activeCamera.transform.position -= dragPosDiff * m_dragSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            m_targetZoomValue -= scroll * m_zoomSpeed;
            m_targetZoomValue = Mathf.Clamp(m_targetZoomValue, m_minZoom, m_maxZoom);
            // Debug.Log(_targetZoomValue);
        }

        //get mouse position before zoom
        Vector3 mousePos1 = Utils.getMouseWorldPosition();

        m_activeCamera.orthographicSize = Mathf.Lerp(m_activeCamera.orthographicSize, m_targetZoomValue, m_smoothZoomValue);

        if (Mathf.RoundToInt(m_activeCamera.orthographicSize) != Mathf.RoundToInt(m_targetZoomValue))
        {
            GUIManager.Instance.showToast($"Zoom {(Mathf.Round((m_maxZoom - m_activeCamera.orthographicSize)/(m_maxZoom - m_initialZoom) * 100))}%", 2f, true);
            // Debug.Log("here");
        }
        //get mouse position after zoom
        Vector3 mousePos2 = Utils.getMouseWorldPosition();
        
        Vector3 mousePositionDiff = mousePos1 - mousePos2;

        Vector3 targetPos = m_activeCamera.transform.position + mousePositionDiff;
        m_activeCamera.transform.position = targetPos;

        //if middle mouse button is clicked, set origin of drag
        if (Input.GetMouseButton(2))
        {
            m_mouseClickedPosition = Input.mousePosition;
            m_isClicked = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            m_isClicked = false;
        }
    }
}
